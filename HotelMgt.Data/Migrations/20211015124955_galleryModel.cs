using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelMgt.Data.Migrations
{
    public partial class galleryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Customers_CustomerId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_CustomerId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Amenities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Amenities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_CustomerId",
                table: "Amenities",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Customers_CustomerId",
                table: "Amenities",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
