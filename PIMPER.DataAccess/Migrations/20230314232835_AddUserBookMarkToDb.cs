using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMPER.Migrations
{
    /// <inheritdoc />
    public partial class AddUserBookMarkToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userBookMarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeTableId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userBookMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userBookMarks_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userBookMarks_recipesTable_RecipeTableId",
                        column: x => x.RecipeTableId,
                        principalTable: "recipesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userBookMarks_ApplicationUserId",
                table: "userBookMarks",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_userBookMarks_RecipeTableId",
                table: "userBookMarks",
                column: "RecipeTableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userBookMarks");
        }
    }
}
