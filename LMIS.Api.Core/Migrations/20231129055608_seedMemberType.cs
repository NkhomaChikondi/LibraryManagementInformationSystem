using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class seedMemberType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_memberTypes_members_memberId",
                table: "memberTypes");

            migrationBuilder.DropIndex(
                name: "IX_memberTypes_memberId",
                table: "memberTypes");

            migrationBuilder.DropColumn(
                name: "memberId",
                table: "memberTypes");

            migrationBuilder.AddColumn<int>(
                name: "MemberTypeId",
                table: "members",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "memberTypes",
                columns: new[] { "Id", "CreatedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3459), "Student" },
                    { 2, new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3460), "Staff" },
                    { 3, new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3462), "Regular Member" },
                    { 4, new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3462), "Premium Member" },
                    { 5, new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3463), "Guest" },
                    { 6, new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3464), "Senior Citezen" },
                    { 7, new DateTime(2023, 11, 29, 5, 56, 7, 800, DateTimeKind.Utc).AddTicks(3465), "Corparate Member" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_members_MemberTypeId",
                table: "members",
                column: "MemberTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_members_memberTypes_MemberTypeId",
                table: "members",
                column: "MemberTypeId",
                principalTable: "memberTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_members_memberTypes_MemberTypeId",
                table: "members");

            migrationBuilder.DropIndex(
                name: "IX_members_MemberTypeId",
                table: "members");

            migrationBuilder.DeleteData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "MemberTypeId",
                table: "members");

            migrationBuilder.AddColumn<int>(
                name: "memberId",
                table: "memberTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_memberTypes_memberId",
                table: "memberTypes",
                column: "memberId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_memberTypes_members_memberId",
                table: "memberTypes",
                column: "memberId",
                principalTable: "members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
