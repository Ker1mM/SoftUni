using Microsoft.EntityFrameworkCore.Migrations;

namespace Rider.Data.Migrations
{
    public partial class DecimalFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Wares",
                type: "decimal(22, 2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Parts",
                type: "decimal(22, 2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                type: "decimal(22, 2)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Wares",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Parts",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22, 2)");
        }
    }
}
