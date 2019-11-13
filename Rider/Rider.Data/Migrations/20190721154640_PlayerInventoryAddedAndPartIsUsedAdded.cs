using Microsoft.EntityFrameworkCore.Migrations;

namespace Rider.Data.Migrations
{
    public partial class PlayerInventoryAddedAndPartIsUsedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Parts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PlayerParts",
                columns: table => new
                {
                    PlayerId = table.Column<string>(nullable: false),
                    PartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerParts", x => new { x.PlayerId, x.PartId });
                    table.ForeignKey(
                        name: "FK_PlayerParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerParts_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerParts_PartId",
                table: "PlayerParts",
                column: "PartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerParts");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Parts");
        }
    }
}
