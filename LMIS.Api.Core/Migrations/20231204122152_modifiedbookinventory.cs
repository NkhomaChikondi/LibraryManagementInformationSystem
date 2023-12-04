using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class modifiedbookinventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookInventories_Book_BookId",
                table: "bookInventories");

            migrationBuilder.DropIndex(
                name: "IX_bookInventories_BookId",
                table: "bookInventories");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 12, 21, 51, 867, DateTimeKind.Utc).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 12, 21, 51, 867, DateTimeKind.Utc).AddTicks(662));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 12, 21, 51, 867, DateTimeKind.Utc).AddTicks(663));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 12, 21, 51, 867, DateTimeKind.Utc).AddTicks(664));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 12, 21, 51, 867, DateTimeKind.Utc).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 12, 21, 51, 867, DateTimeKind.Utc).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 12, 21, 51, 867, DateTimeKind.Utc).AddTicks(666));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_bookInventories_BookId",
                table: "bookInventories",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookInventories_Book_BookId",
                table: "bookInventories",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
