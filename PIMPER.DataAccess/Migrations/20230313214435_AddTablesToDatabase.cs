using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMPER.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quantitiesTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quantitiesTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "recipesTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unitTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unitTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ingredientTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitTableId = table.Column<int>(type: "int", nullable: false),
                    QuantitiesTableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredientTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ingredientTables_quantitiesTables_QuantitiesTableId",
                        column: x => x.QuantitiesTableId,
                        principalTable: "quantitiesTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ingredientTables_unitTables_UnitTableId",
                        column: x => x.UnitTableId,
                        principalTable: "unitTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipeIngredientTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientTableId = table.Column<int>(type: "int", nullable: false),
                    RecipeTableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipeIngredientTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recipeIngredientTables_ingredientTables_IngredientTableId",
                        column: x => x.IngredientTableId,
                        principalTable: "ingredientTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recipeIngredientTables_recipesTable_RecipeTableId",
                        column: x => x.RecipeTableId,
                        principalTable: "recipesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingredientTables_QuantitiesTableId",
                table: "ingredientTables",
                column: "QuantitiesTableId");

            migrationBuilder.CreateIndex(
                name: "IX_ingredientTables_UnitTableId",
                table: "ingredientTables",
                column: "UnitTableId");

            migrationBuilder.CreateIndex(
                name: "IX_recipeIngredientTables_IngredientTableId",
                table: "recipeIngredientTables",
                column: "IngredientTableId");

            migrationBuilder.CreateIndex(
                name: "IX_recipeIngredientTables_RecipeTableId",
                table: "recipeIngredientTables",
                column: "RecipeTableId");
        }

        /// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropTable(
        //        name: "recipeIngredientTables");

        //    migrationBuilder.DropTable(
        //        name: "ingredientTables");

        //    migrationBuilder.DropTable(
        //        name: "recipesTable");

        //    migrationBuilder.DropTable(
        //        name: "quantitiesTables");

        //    migrationBuilder.DropTable(
        //        name: "unitTables");
        //}
    }
}
