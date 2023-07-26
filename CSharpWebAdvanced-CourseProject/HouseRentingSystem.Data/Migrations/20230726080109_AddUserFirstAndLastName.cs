using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class AddUserFirstAndLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("3b201b24-38d6-403f-abfc-8129059e0912"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("56f41d64-73a2-4732-aae2-1cb0b1100a72"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("a2d9cef4-5a2a-4fd8-ab9f-5c3059911aba"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Test");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Testov");

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("25b5200d-000c-4bf0-8915-429f9fa721fe"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("5bafae9d-fdfd-45cb-98b5-9981b0e408ca"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("8b2f8832-1255-4c55-b9bb-55e3552f67ff"), "North London, UK (near the border)", new Guid("5bafae9d-fdfd-45cb-98b5-9981b0e408ca"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://cdn.remax.co.za/listings/4007973/original/cd80cc39-4125-d73e-754c-c8288c962867.jpg", 2100.00m, new Guid("e68c56fb-6db2-4c11-62ed-08db7879bc1d"), "Big House Marina" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("d0284db1-d0a0-45a9-abec-180780eae8f9"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("5bafae9d-fdfd-45cb-98b5-9981b0e408ca"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("25b5200d-000c-4bf0-8915-429f9fa721fe"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("8b2f8832-1255-4c55-b9bb-55e3552f67ff"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("d0284db1-d0a0-45a9-abec-180780eae8f9"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title", "isActive" },
                values: new object[] { new Guid("3b201b24-38d6-403f-abfc-8129059e0912"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("5bafae9d-fdfd-45cb-98b5-9981b0e408ca"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House", false });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title", "isActive" },
                values: new object[] { new Guid("56f41d64-73a2-4732-aae2-1cb0b1100a72"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("5bafae9d-fdfd-45cb-98b5-9981b0e408ca"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort", false });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title", "isActive" },
                values: new object[] { new Guid("a2d9cef4-5a2a-4fd8-ab9f-5c3059911aba"), "North London, UK (near the border)", new Guid("5bafae9d-fdfd-45cb-98b5-9981b0e408ca"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://cdn.remax.co.za/listings/4007973/original/cd80cc39-4125-d73e-754c-c8288c962867.jpg", 2100.00m, new Guid("e68c56fb-6db2-4c11-62ed-08db7879bc1d"), "Big House Marina", false });
        }
    }
}
