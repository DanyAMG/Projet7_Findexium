using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P7CreateRestApi.Migrations
{
    /// <inheritdoc />
    public partial class SuppressionTradeDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradeDate",
                table: "Trades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TradeDate",
                table: "Trades",
                type: "real",
                nullable: true);
        }
    }
}
