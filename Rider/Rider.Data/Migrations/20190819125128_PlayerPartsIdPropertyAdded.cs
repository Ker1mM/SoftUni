using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rider.Data.Migrations
{
    public partial class PlayerPartsIdPropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerParts",
                table: "PlayerParts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PlayerParts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerParts",
                table: "PlayerParts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerParts_PlayerId",
                table: "PlayerParts",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerParts",
                table: "PlayerParts");

            migrationBuilder.DropIndex(
                name: "IX_PlayerParts_PlayerId",
                table: "PlayerParts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlayerParts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerParts",
                table: "PlayerParts",
                columns: new[] { "PlayerId", "PartId" });
        }
    }
}
