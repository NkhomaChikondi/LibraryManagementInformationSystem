using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedkeytomemberandMembertype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 6, 15, 54, 784, DateTimeKind.Utc).AddTicks(9137));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 6, 15, 54, 784, DateTimeKind.Utc).AddTicks(9139));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 6, 15, 54, 784, DateTimeKind.Utc).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 6, 15, 54, 784, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 6, 15, 54, 784, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 6, 15, 54, 784, DateTimeKind.Utc).AddTicks(9142));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 6, 15, 54, 784, DateTimeKind.Utc).AddTicks(9143));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3459));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3460));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3462));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3462));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3463));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3464));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3465));
        }
    }
}
