using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockQuoteChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5afbe4a4-cc43-4c60-9b3b-f9b227128b6d"), "Room Two" },
                    { new Guid("8bb6e697-973f-43e9-8e16-34f4ba4f7973"), "Room One" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { new Guid("08a0bb60-cbc9-46c4-a3e5-207564fdcf0f"), "userone@email.com", "ChatUserOne", "", "123" },
                    { new Guid("a31137cb-7ceb-464a-a0f9-e94476660979"), "usertwo@email.com", "ChatUserTwo", "", "123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("5afbe4a4-cc43-4c60-9b3b-f9b227128b6d"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("8bb6e697-973f-43e9-8e16-34f4ba4f7973"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("08a0bb60-cbc9-46c4-a3e5-207564fdcf0f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a31137cb-7ceb-464a-a0f9-e94476660979"));
        }
    }
}
