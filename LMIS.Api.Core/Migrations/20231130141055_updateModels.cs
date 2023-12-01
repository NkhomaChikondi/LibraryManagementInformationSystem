using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class updateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkoutTransactions_bookInventories_BookInventoryId",
                table: "checkoutTransactions");

            migrationBuilder.DropIndex(
                name: "IX_checkoutTransactions_BookInventoryId",
                table: "checkoutTransactions");

            migrationBuilder.AddColumn<int>(
                name: "ChckOutTransId",
                table: "bookInventories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_bookInventories_ChckOutTransId",
                table: "bookInventories",
                column: "ChckOutTransId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookInventories_checkoutTransactions_ChckOutTransId",
                table: "bookInventories",
                column: "ChckOutTransId",
                principalTable: "checkoutTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookInventories_checkoutTransactions_ChckOutTransId",
                table: "bookInventories");

            migrationBuilder.DropIndex(
                name: "IX_bookInventories_ChckOutTransId",
                table: "bookInventories");

            migrationBuilder.DropColumn(
                name: "ChckOutTransId",
                table: "bookInventories");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9060));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9062));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9063));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9064));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9065));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9066));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9066));

            migrationBuilder.CreateIndex(
                name: "IX_checkoutTransactions_BookInventoryId",
                table: "checkoutTransactions",
                column: "BookInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_checkoutTransactions_bookInventories_BookInventoryId",
                table: "checkoutTransactions",
                column: "BookInventoryId",
                principalTable: "bookInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
