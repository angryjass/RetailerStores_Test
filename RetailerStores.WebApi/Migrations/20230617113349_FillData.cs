using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using RetailerStores.Domain;
using System.Xml.Linq;

#nullable disable

namespace RetailerStores.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class FillData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ExcelParser.ExcelParser.FillData("../Site Data.xlsx");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE \"Store\", \"Stock\", \"Manager\"");
        }
    }
}
