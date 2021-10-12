using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelMgt.Data.Migrations
{
    public partial class relationsgipInBookingandAmenityCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "Amenities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_BookingId",
                table: "Amenities",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Bookings_BookingId",
                table: "Amenities",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Bookings_BookingId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_BookingId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Amenities");
        }
    }
}
