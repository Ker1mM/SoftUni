using Microsoft.EntityFrameworkCore.Migrations;

namespace Rider.Data.Migrations
{
    public partial class TrackEntityUpdateIsActiveAndIsArchivedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tracks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Tracks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Tracks");
        }
    }
}
