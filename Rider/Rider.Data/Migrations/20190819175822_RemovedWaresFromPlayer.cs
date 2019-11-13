using Microsoft.EntityFrameworkCore.Migrations;

namespace Rider.Data.Migrations
{
    public partial class RemovedWaresFromPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wares_AspNetUsers_PlayerId",
                table: "Wares");

            migrationBuilder.DropIndex(
                name: "IX_Wares_PlayerId",
                table: "Wares");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Wares");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Wares",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wares_PlayerId",
                table: "Wares",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_AspNetUsers_PlayerId",
                table: "Wares",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
