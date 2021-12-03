using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stonks.Db.MigrationsStocks
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StockSymbol = table.Column<string>(nullable: true),
                    Exchange = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: false),
                    HasDataFrom = table.Column<DateTime>(nullable: false),
                    HasDataTo = table.Column<DateTime>(nullable: false),
                    DataJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
