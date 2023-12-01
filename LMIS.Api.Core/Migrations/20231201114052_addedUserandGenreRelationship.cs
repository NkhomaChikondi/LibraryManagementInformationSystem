using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class addedUserandGenreRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "genres",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 40, 51, 424, DateTimeKind.Utc).AddTicks(4592));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 40, 51, 424, DateTimeKind.Utc).AddTicks(4595));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 40, 51, 424, DateTimeKind.Utc).AddTicks(4596));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 40, 51, 424, DateTimeKind.Utc).AddTicks(4598));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 40, 51, 424, DateTimeKind.Utc).AddTicks(4599));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 40, 51, 424, DateTimeKind.Utc).AddTicks(4600));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 40, 51, 424, DateTimeKind.Utc).AddTicks(4601));

            migrationBuilder.CreateIndex(
                name: "IX_genres_userId",
                table: "genres",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_genres_applicationUsers_userId",
                table: "genres",
                column: "userId",
                principalTable: "applicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_genres_applicationUsers_userId",
                table: "genres");

            migrationBuilder.DropIndex(
                name: "IX_genres_userId",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "genres");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8405));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8406));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8407));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8407));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8408));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 1, 11, 18, 7, 467, DateTimeKind.Utc).AddTicks(8409));
        }
    }
}
