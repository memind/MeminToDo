using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FoodsUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Calorie = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 10, 8, 19, 5, 17, 45, DateTimeKind.Local).AddTicks(8215)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealsUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    Calorie = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 10, 8, 19, 5, 17, 46, DateTimeKind.Local).AddTicks(1707)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodMeal",
                columns: table => new
                {
                    FoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMeal", x => new { x.FoodsId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_FoodMeal_FoodTable_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "FoodTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodMeal_MealTable_MealsId",
                        column: x => x.MealsId,
                        principalTable: "MealTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodMeal_MealsId",
                table: "FoodMeal",
                column: "MealsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodMeal");

            migrationBuilder.DropTable(
                name: "FoodTable");

            migrationBuilder.DropTable(
                name: "MealTable");
        }
    }
}
