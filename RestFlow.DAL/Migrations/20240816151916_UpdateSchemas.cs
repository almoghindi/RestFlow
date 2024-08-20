using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestFlow.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 1, "For the beggining of the meal", "Starters" });
        }
    }
}
