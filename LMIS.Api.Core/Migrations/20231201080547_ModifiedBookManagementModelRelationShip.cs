using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedBookManagementModelRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "BookInventoryId",
                table: "checkoutTransactions",
                newName: "bookInventoryId");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 8, 5, 46, 962, DateTimeKind.Utc).AddTicks(9019));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 8, 5, 46, 962, DateTimeKind.Utc).AddTicks(9021));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 8, 5, 46, 962, DateTimeKind.Utc).AddTicks(9022));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 8, 5, 46, 962, DateTimeKind.Utc).AddTicks(9023));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 8, 5, 46, 962, DateTimeKind.Utc).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 8, 5, 46, 962, DateTimeKind.Utc).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 8, 5, 46, 962, DateTimeKind.Utc).AddTicks(9025));

            migrationBuilder.CreateIndex(
                name: "IX_checkoutTransactions_bookInventoryId",
                table: "checkoutTransactions",
                column: "bookInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_checkoutTransactions_bookInventories_bookInventoryId",
                table: "checkoutTransactions",
                column: "bookInventoryId",
                principalTable: "bookInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkoutTransactions_bookInventories_bookInventoryId",
                table: "checkoutTransactions");

            migrationBuilder.DropIndex(
                name: "IX_checkoutTransactions_bookInventoryId",
                table: "checkoutTransactions");

            migrationBuilder.RenameColumn(
                name: "bookInventoryId",
                table: "checkoutTransactions",
                newName: "BookInventoryId");

            migrationBuilder.AddColumn<int>(
                name: "ChckOutTransId",
                table: "bookInventories",
                type: "integer",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_bookInventories_ChckOutTransId",
                table: "bookInventories",
                column: "ChckOutTransId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookInventories_checkoutTransactions_ChckOutTransId",
                table: "bookInventories",
                column: "ChckOutTransId",
                principalTable: "checkoutTransactions",
                principalColumn: "Id");
        }
    }
}
