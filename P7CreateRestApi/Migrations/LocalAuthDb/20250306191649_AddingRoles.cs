using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P7CreateRestApi.Migrations.LocalAuthDb
{
    /// <inheritdoc />
    public partial class AddingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1edcdfe0-1a9e-494d-905e-ea86d25b01ce", "1edcdfe0-1a9e-494d-905e-ea86d25b01ce", "Visitor", "VISITOR" },
                    { "3e82d213-759f-4ec1-abe4-33225dc028e6", "3e82d213-759f-4ec1-abe4-33225dc028e6", "Technician", "TECHNICIAN" },
                    { "995536c0-b1a7-4d3d-814e-659b86fe854e", "995536c0-b1a7-4d3d-814e-659b86fe854e", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1edcdfe0-1a9e-494d-905e-ea86d25b01ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e82d213-759f-4ec1-abe4-33225dc028e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "995536c0-b1a7-4d3d-814e-659b86fe854e");
        }
    }
}
