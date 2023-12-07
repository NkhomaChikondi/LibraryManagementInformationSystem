using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class modifyTempdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "temp_data",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 42, 53, 675, DateTimeKind.Utc).AddTicks(9038));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 42, 53, 675, DateTimeKind.Utc).AddTicks(9039));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 42, 53, 675, DateTimeKind.Utc).AddTicks(9041));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 42, 53, 675, DateTimeKind.Utc).AddTicks(9042));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 42, 53, 675, DateTimeKind.Utc).AddTicks(9043));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 42, 53, 675, DateTimeKind.Utc).AddTicks(9044));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 42, 53, 675, DateTimeKind.Utc).AddTicks(9044));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "temp_data",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 30, 43, 3, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 30, 43, 3, DateTimeKind.Utc).AddTicks(9922));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 30, 43, 3, DateTimeKind.Utc).AddTicks(9923));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 30, 43, 3, DateTimeKind.Utc).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 30, 43, 3, DateTimeKind.Utc).AddTicks(9925));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 30, 43, 3, DateTimeKind.Utc).AddTicks(9926));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 6, 22, 30, 43, 3, DateTimeKind.Utc).AddTicks(9927));
        }
    }
}
