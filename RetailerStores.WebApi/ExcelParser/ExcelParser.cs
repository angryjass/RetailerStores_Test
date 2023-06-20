using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication;
using RetailerStores.Application.Interfaces;
using RetailerStores.Domain;
using RetailerStores.WebApi.Contexts;

namespace RetailerStores.WebApi.ExcelParser
{
    public static class ExcelParser
    {
        private static readonly IRetailerStoresDbContext _dbContext = new DesignTimeDbContextFactory().CreateDbContext(Array.Empty<string>());
        private static readonly ISystemClock _systemClock = new SystemClock();

        public static void FillData(string path)
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(path, false))
            {
                //for correct decimal parse
                System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-EN");

                if (spreadsheetDocument.WorkbookPart == null)
                    return;

                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                foreach (Row row in sheetData.Elements<Row>().Skip(1))
                {
                    //parsing
                    var newStore = new Store()
                    {
                        Name = GetStringValue((Cell)row.ElementAt(0), workbookPart),
                        CountryCode = GetStringValue((Cell)row.ElementAt(1), workbookPart),
                        StoreEmail = GetStringValue((Cell)row.ElementAt(2), workbookPart)
                    };
                    var newManager = new Manager()
                    {
                        FirstName = GetStringValue((Cell)row.ElementAt(3), workbookPart),
                        LastName = GetStringValue((Cell)row.ElementAt(4), workbookPart),
                        Email = GetStringValue((Cell)row.ElementAt(5), workbookPart),
                        Category = GetIntValue((Cell)row.ElementAt(6), workbookPart)
                    };
                    var newStock = new Stock()
                    {
                        Backstock = GetIntValue((Cell)row.ElementAt(7), workbookPart),
                        Frontstock = GetIntValue((Cell)row.ElementAt(8), workbookPart),
                        ShoppingWindow = GetIntValue((Cell)row.ElementAt(9), workbookPart),
                        Accuracy = GetDecimalValue((Cell)row.ElementAt(10), workbookPart),
                        OnFloorAvailability = GetDecimalValue((Cell)row.ElementAt(11), workbookPart),
                        MeanAge = GetIntValue((Cell)row.ElementAt(12), workbookPart),
                        RecordingDate = _systemClock.UtcNow
                    };

                    //relations
                    newStore.Stocks.Add(newStock);
                    newStore.Manager = newManager;

                    _dbContext.Set<Store>().Add(newStore);
                    _dbContext.Set<Stock>().Add(newStock);
                    _dbContext.Set<Manager>().Add(newManager);
                }

                _dbContext.SaveChangesAsync(new CancellationToken()).Wait();
            }

        }

        private static string GetStringValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell.DataType == null)
                return string.Empty;

            string value = cell.InnerText;
            if (cell.DataType.Value == CellValues.SharedString)
            {
                var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>()
                    .FirstOrDefault();

                if (stringTable != null)
                    value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
            }

            return value;
        }

        private static int GetIntValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell.DataType != null) return 0;

            int.TryParse(cell.InnerText, out int value);

            return value;
        }

        private static decimal GetDecimalValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell.DataType != null) return 0;

            decimal.TryParse(cell.InnerText, out decimal value);

            return value;
        }
    }
}
