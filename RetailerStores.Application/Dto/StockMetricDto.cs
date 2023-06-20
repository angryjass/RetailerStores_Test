namespace RetailerStores.Application.Dto
{
    public class StockMetricDto
    {
        /// <summary>
        /// Min, avg and max values
        /// </summary>
        public decimal[] Values { get; set; } = Array.Empty<decimal>();
    }
}
