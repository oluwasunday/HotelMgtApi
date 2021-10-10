using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelMgt.Data.Migrations
{
    public partial class modelIDandCreatedAtInitialized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "RoomTypes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "RoomTypes");
        }
    }
}
