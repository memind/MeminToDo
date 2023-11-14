using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetAccountTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountsUserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 15, 0, 44, 9, 583, DateTimeKind.Local).AddTicks(4828))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetAccountTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyFlowTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudgetsUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 15, 0, 44, 9, 583, DateTimeKind.Local).AddTicks(7749)),
                    BudgetAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyFlowTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyFlowTable_BudgetAccountTable_BudgetAccountId",
                        column: x => x.BudgetAccountId,
                        principalTable: "BudgetAccountTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletName = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 15, 0, 44, 9, 583, DateTimeKind.Local).AddTicks(6360)),
                    BudgetAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTable_BudgetAccountTable_BudgetAccountId",
                        column: x => x.BudgetAccountId,
                        principalTable: "BudgetAccountTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyFlowTable_BudgetAccountId",
                table: "MoneyFlowTable",
                column: "BudgetAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTable_BudgetAccountId",
                table: "WalletTable",
                column: "BudgetAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyFlowTable");

            migrationBuilder.DropTable(
                name: "WalletTable");

            migrationBuilder.DropTable(
                name: "BudgetAccountTable");
        }
    }
}
