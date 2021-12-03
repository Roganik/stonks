using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stonks.Db.MigrationsFantasy
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FantasyTeams",
                columns: table => new
                {
                    FantasyTeamId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyTeams", x => x.FantasyTeamId);
                });

            migrationBuilder.CreateTable(
                name: "InvestReviewSources",
                columns: table => new
                {
                    InvestReviewSourceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestReviewSources", x => x.InvestReviewSourceId);
                });

            migrationBuilder.CreateTable(
                name: "InvestReviewPosts",
                columns: table => new
                {
                    InvestReviewPostId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExternalId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    InvestReviewSourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestReviewPosts", x => x.InvestReviewPostId);
                    table.ForeignKey(
                        name: "FK_InvestReviewPosts_InvestReviewSources_InvestReviewSourceId",
                        column: x => x.InvestReviewSourceId,
                        principalTable: "InvestReviewSources",
                        principalColumn: "InvestReviewSourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FantasyTeamMembers",
                columns: table => new
                {
                    FantasyTeamMemberId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StockSymbol = table.Column<string>(nullable: true),
                    Exchange = table.Column<string>(nullable: true),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    PriceOnPublicationDate = table.Column<decimal>(nullable: false),
                    PriceDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    InvestReviewPostId = table.Column<int>(nullable: false),
                    FantasyTeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FantasyTeamMembers", x => x.FantasyTeamMemberId);
                    table.ForeignKey(
                        name: "FK_FantasyTeamMembers_FantasyTeams_FantasyTeamId",
                        column: x => x.FantasyTeamId,
                        principalTable: "FantasyTeams",
                        principalColumn: "FantasyTeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FantasyTeamMembers_InvestReviewPosts_InvestReviewPostId",
                        column: x => x.InvestReviewPostId,
                        principalTable: "InvestReviewPosts",
                        principalColumn: "InvestReviewPostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamMembers_FantasyTeamId",
                table: "FantasyTeamMembers",
                column: "FantasyTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FantasyTeamMembers_InvestReviewPostId",
                table: "FantasyTeamMembers",
                column: "InvestReviewPostId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestReviewPosts_InvestReviewSourceId",
                table: "InvestReviewPosts",
                column: "InvestReviewSourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FantasyTeamMembers");

            migrationBuilder.DropTable(
                name: "FantasyTeams");

            migrationBuilder.DropTable(
                name: "InvestReviewPosts");

            migrationBuilder.DropTable(
                name: "InvestReviewSources");
        }
    }
}
