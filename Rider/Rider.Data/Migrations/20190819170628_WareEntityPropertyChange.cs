using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rider.Data.Migrations
{
    public partial class WareEntityPropertyChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeParts_Bikes_BikeId",
                table: "BikeParts");

            migrationBuilder.DropForeignKey(
                name: "FK_BikeParts_Parts_PartId",
                table: "BikeParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Wares_Parts_PartId",
                table: "Wares");

            migrationBuilder.DropForeignKey(
                name: "FK_Wares_AspNetUsers_SellerId",
                table: "Wares");

            migrationBuilder.DropIndex(
                name: "IX_Wares_SellerId",
                table: "Wares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BikeParts",
                table: "BikeParts");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Wares");

            migrationBuilder.RenameColumn(
                name: "PartId",
                table: "Wares",
                newName: "PlayerPartId");

            migrationBuilder.RenameIndex(
                name: "IX_Wares_PartId",
                table: "Wares",
                newName: "IX_Wares_PlayerPartId");

            migrationBuilder.RenameColumn(
                name: "PartId",
                table: "BikeParts",
                newName: "PlayerPartId");

            migrationBuilder.RenameIndex(
                name: "IX_BikeParts_PartId",
                table: "BikeParts",
                newName: "IX_BikeParts_PlayerPartId");

            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Wares",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BikePartId",
                table: "PlayerParts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BikeParts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BikeParts",
                table: "BikeParts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wares_PlayerId",
                table: "Wares",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerParts_BikePartId",
                table: "PlayerParts",
                column: "BikePartId");

            migrationBuilder.CreateIndex(
                name: "IX_BikeParts_BikeId",
                table: "BikeParts",
                column: "BikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeParts_Bikes_BikeId",
                table: "BikeParts",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BikeParts_PlayerParts_PlayerPartId",
                table: "BikeParts",
                column: "PlayerPartId",
                principalTable: "PlayerParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerParts_BikeParts_BikePartId",
                table: "PlayerParts",
                column: "BikePartId",
                principalTable: "BikeParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_AspNetUsers_PlayerId",
                table: "Wares",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_PlayerParts_PlayerPartId",
                table: "Wares",
                column: "PlayerPartId",
                principalTable: "PlayerParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeParts_Bikes_BikeId",
                table: "BikeParts");

            migrationBuilder.DropForeignKey(
                name: "FK_BikeParts_PlayerParts_PlayerPartId",
                table: "BikeParts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerParts_BikeParts_BikePartId",
                table: "PlayerParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Wares_AspNetUsers_PlayerId",
                table: "Wares");

            migrationBuilder.DropForeignKey(
                name: "FK_Wares_PlayerParts_PlayerPartId",
                table: "Wares");

            migrationBuilder.DropIndex(
                name: "IX_Wares_PlayerId",
                table: "Wares");

            migrationBuilder.DropIndex(
                name: "IX_PlayerParts_BikePartId",
                table: "PlayerParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BikeParts",
                table: "BikeParts");

            migrationBuilder.DropIndex(
                name: "IX_BikeParts_BikeId",
                table: "BikeParts");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Wares");

            migrationBuilder.DropColumn(
                name: "BikePartId",
                table: "PlayerParts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BikeParts");

            migrationBuilder.RenameColumn(
                name: "PlayerPartId",
                table: "Wares",
                newName: "PartId");

            migrationBuilder.RenameIndex(
                name: "IX_Wares_PlayerPartId",
                table: "Wares",
                newName: "IX_Wares_PartId");

            migrationBuilder.RenameColumn(
                name: "PlayerPartId",
                table: "BikeParts",
                newName: "PartId");

            migrationBuilder.RenameIndex(
                name: "IX_BikeParts_PlayerPartId",
                table: "BikeParts",
                newName: "IX_BikeParts_PartId");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Wares",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BikeParts",
                table: "BikeParts",
                columns: new[] { "BikeId", "PartId" });

            migrationBuilder.CreateIndex(
                name: "IX_Wares_SellerId",
                table: "Wares",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeParts_Bikes_BikeId",
                table: "BikeParts",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BikeParts_Parts_PartId",
                table: "BikeParts",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_Parts_PartId",
                table: "Wares",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_AspNetUsers_SellerId",
                table: "Wares",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
