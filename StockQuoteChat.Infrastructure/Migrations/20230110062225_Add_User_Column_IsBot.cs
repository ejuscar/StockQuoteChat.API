using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockQuoteChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserColumnIsBot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("3d47ff74-c701-4c5e-ad96-8c5dba9452a7"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("e087c588-23c6-4831-aaf5-fe4e0fe2d37e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5663a1bb-32a6-4456-97b6-d7af18a46f4b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ee0312b-172f-4cd5-a95a-102dd8e1aa0e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8f97f319-9056-4a5c-a3a5-7599f07c28e6"));

            migrationBuilder.AddColumn<bool>(
                name: "IsBot",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("070344f1-018a-4443-8732-0a87ec7a5aa7"), "Room Two" },
                    { new Guid("3913a673-5f6b-4177-938f-3d83516482d7"), "Room One" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsBot", "LastName", "Password" },
                values: new object[,]
                {
                    { new Guid("6ae683b2-cdad-40c9-be04-34792ae196e3"), "userone@email.com", "ChatUserOne", false, "", "123" },
                    { new Guid("98085ba7-b79a-4d54-b857-9a7aa302bf19"), "mychatbot@chatmail.com", "Bot", true, "", "botexamplepassword" },
                    { new Guid("c8233f80-bc22-4886-9f26-64878f554da8"), "usertwo@email.com", "ChatUserTwo", false, "", "123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("070344f1-018a-4443-8732-0a87ec7a5aa7"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("3913a673-5f6b-4177-938f-3d83516482d7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6ae683b2-cdad-40c9-be04-34792ae196e3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98085ba7-b79a-4d54-b857-9a7aa302bf19"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c8233f80-bc22-4886-9f26-64878f554da8"));

            migrationBuilder.DropColumn(
                name: "IsBot",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3d47ff74-c701-4c5e-ad96-8c5dba9452a7"), "Room Two" },
                    { new Guid("e087c588-23c6-4831-aaf5-fe4e0fe2d37e"), "Room One" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { new Guid("5663a1bb-32a6-4456-97b6-d7af18a46f4b"), "usertwo@email.com", "ChatUserTwo", "", "123" },
                    { new Guid("7ee0312b-172f-4cd5-a95a-102dd8e1aa0e"), "userone@email.com", "ChatUserOne", "", "123" },
                    { new Guid("8f97f319-9056-4a5c-a3a5-7599f07c28e6"), "mychatbot@chatmail.com", "Bot", "", "botexamplepassword" }
                });
        }
    }
}
