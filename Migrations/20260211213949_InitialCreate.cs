using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LayoutPlannerPOC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FactoryComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ImageFilePath = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<int>(type: "INTEGER", nullable: true),
                    Width = table.Column<int>(type: "INTEGER", nullable: true),
                    Rotation = table.Column<int>(type: "INTEGER", nullable: true),
                    X = table.Column<int>(type: "INTEGER", nullable: true),
                    Y = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryComponents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FactoryComponents",
                columns: new[] { "Id", "Height", "ImageFilePath", "Name", "Rotation", "Width", "X", "Y" },
                values: new object[,]
                {
                    { 1, 10, null, "Constructor", 0, 5, 10, 0 },
                    { 2, 4, null, "Smelter", 0, 10, 15, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactoryComponents");
        }
    }
}
