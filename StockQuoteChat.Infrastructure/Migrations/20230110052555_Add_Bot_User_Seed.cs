using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockQuoteChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBotUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("4533c28e-1d83-45e5-bf1e-9592312e7b07"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("df626fef-3144-419d-97a0-36060d627b27"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3c2f3df4-9439-4695-b166-5d4bda902e69"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5df068f3-0e41-4748-ab42-e349cad71089"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4533c28e-1d83-45e5-bf1e-9592312e7b07"), "Room One" },
                    { new Guid("df626fef-3144-419d-97a0-36060d627b27"), "Room Two" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { new Guid("3c2f3df4-9439-4695-b166-5d4bda902e69"), "usertwo@email.com", "ChatUserTwo", "", "123" },
                    { new Guid("5df068f3-0e41-4748-ab42-e349cad71089"), "userone@email.com", "ChatUserOne", "", "123" }
                });
        }
    }
}
