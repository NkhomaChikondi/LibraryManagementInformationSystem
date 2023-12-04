using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetNotifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Messsage = table.Column<int>(type: "integer", nullable: false),
                    checkoutTransactionId = table.Column<int>(type: "integer", nullable: false),
                    memberId = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetNotifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_GetNotifications_applicationUsers_userId",
                        column: x => x.userId,
                        principalTable: "applicationUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GetNotifications_checkoutTransactions_checkoutTransactionId",
                        column: x => x.checkoutTransactionId,
                        principalTable: "checkoutTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GetNotifications_members_memberId",
                        column: x => x.memberId,
                        principalTable: "members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_GetNotifications_checkoutTransactionId",
                table: "GetNotifications",
                column: "checkoutTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_GetNotifications_memberId",
                table: "GetNotifications",
                column: "memberId");

            migrationBuilder.CreateIndex(
                name: "IX_GetNotifications_userId",
                table: "GetNotifications",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetNotifications");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 11, 11, 437, DateTimeKind.Utc).AddTicks(9480));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 11, 11, 437, DateTimeKind.Utc).AddTicks(9481));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 11, 11, 437, DateTimeKind.Utc).AddTicks(9482));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 11, 11, 437, DateTimeKind.Utc).AddTicks(9483));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 11, 11, 437, DateTimeKind.Utc).AddTicks(9484));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 11, 11, 437, DateTimeKind.Utc).AddTicks(9484));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 4, 8, 11, 11, 437, DateTimeKind.Utc).AddTicks(9485));
        }
    }
}
