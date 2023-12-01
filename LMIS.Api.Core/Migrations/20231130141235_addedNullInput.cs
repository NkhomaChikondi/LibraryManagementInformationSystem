using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class addedNullInput : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookInventories_checkoutTransactions_ChckOutTransId",
                table: "bookInventories");

            migrationBuilder.AlterColumn<int>(
                name: "ChckOutTransId",
                table: "bookInventories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 12, 34, 705, DateTimeKind.Utc).AddTicks(1684));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 12, 34, 705, DateTimeKind.Utc).AddTicks(1686));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 12, 34, 705, DateTimeKind.Utc).AddTicks(1687));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 12, 34, 705, DateTimeKind.Utc).AddTicks(1688));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 12, 34, 705, DateTimeKind.Utc).AddTicks(1688));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 12, 34, 705, DateTimeKind.Utc).AddTicks(1689));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 12, 34, 705, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.AddForeignKey(
                name: "FK_bookInventories_checkoutTransactions_ChckOutTransId",
                table: "bookInventories",
                column: "ChckOutTransId",
                principalTable: "checkoutTransactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookInventories_checkoutTransactions_ChckOutTransId",
                table: "bookInventories");

            migrationBuilder.AlterColumn<int>(
                name: "ChckOutTransId",
                table: "bookInventories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 10, 54, 726, DateTimeKind.Utc).AddTicks(5712));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 10, 54, 726, DateTimeKind.Utc).AddTicks(5714));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 10, 54, 726, DateTimeKind.Utc).AddTicks(5715));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 10, 54, 726, DateTimeKind.Utc).AddTicks(5716));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 10, 54, 726, DateTimeKind.Utc).AddTicks(5717));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 10, 54, 726, DateTimeKind.Utc).AddTicks(5718));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 10, 54, 726, DateTimeKind.Utc).AddTicks(5718));

            migrationBuilder.AddForeignKey(
                name: "FK_bookInventories_checkoutTransactions_ChckOutTransId",
                table: "bookInventories",
                column: "ChckOutTransId",
                principalTable: "checkoutTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
