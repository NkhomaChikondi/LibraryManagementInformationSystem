using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class modifiedModelRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "checkoutTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "Messsage",
                table: "GetNotifications",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 10, 1, 33, 340, DateTimeKind.Utc).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 10, 1, 33, 340, DateTimeKind.Utc).AddTicks(3411));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 10, 1, 33, 340, DateTimeKind.Utc).AddTicks(3412));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 10, 1, 33, 340, DateTimeKind.Utc).AddTicks(3413));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 10, 1, 33, 340, DateTimeKind.Utc).AddTicks(3413));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 10, 1, 33, 340, DateTimeKind.Utc).AddTicks(3414));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 10, 1, 33, 340, DateTimeKind.Utc).AddTicks(3415));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "checkoutTransactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Messsage",
                table: "GetNotifications",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 47, 31, 432, DateTimeKind.Utc).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 47, 31, 432, DateTimeKind.Utc).AddTicks(6436));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 47, 31, 432, DateTimeKind.Utc).AddTicks(6437));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 47, 31, 432, DateTimeKind.Utc).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 47, 31, 432, DateTimeKind.Utc).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 47, 31, 432, DateTimeKind.Utc).AddTicks(6439));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 47, 31, 432, DateTimeKind.Utc).AddTicks(6440));
        }
    }
}
