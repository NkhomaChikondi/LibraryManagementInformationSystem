using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class chengedtablesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetNotifications_applicationUsers_userId",
                table: "GetNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_GetNotifications_checkoutTransactions_checkoutTransactionId",
                table: "GetNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_GetNotifications_members_memberId",
                table: "GetNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_checkoutTransactions_applicationUsers_UserId",
                table: "checkoutTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_checkoutTransactions_bookInventories_bookInventoryId",
                table: "checkoutTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_checkoutTransactions_members_MemberId",
                table: "checkoutTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_genres_applicationUsers_userId",
                table: "genres");

            migrationBuilder.DropForeignKey(
                name: "FK_members_applicationUsers_userId",
                table: "members");

            migrationBuilder.DropForeignKey(
                name: "FK_members_memberTypes_MemberTypeId",
                table: "members");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_applicationUsers_userId",
                table: "userRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_roles_roleId",
                table: "userRoles");

            migrationBuilder.DropTable(
                name: "temp_data");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_members",
                table: "members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_memberTypes",
                table: "memberTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_genres",
                table: "genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_checkoutTransactions",
                table: "checkoutTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookInventories",
                table: "bookInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_applicationUsers",
                table: "applicationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetNotifications",
                table: "GetNotifications");

            migrationBuilder.RenameTable(
                name: "userRoles",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "members",
                newName: "Members");

            migrationBuilder.RenameTable(
                name: "memberTypes",
                newName: "MemberTypes");

            migrationBuilder.RenameTable(
                name: "genres",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "checkoutTransactions",
                newName: "CheckoutTransactions");

            migrationBuilder.RenameTable(
                name: "bookInventories",
                newName: "BookInventories");

            migrationBuilder.RenameTable(
                name: "applicationUsers",
                newName: "ApplicationUsers");

            migrationBuilder.RenameTable(
                name: "GetNotifications",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_userRoles_roleId",
                table: "UserRoles",
                newName: "IX_UserRoles_roleId");

            migrationBuilder.RenameIndex(
                name: "IX_members_userId",
                table: "Members",
                newName: "IX_Members_userId");

            migrationBuilder.RenameIndex(
                name: "IX_members_MemberTypeId",
                table: "Members",
                newName: "IX_Members_MemberTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_genres_userId",
                table: "Genres",
                newName: "IX_Genres_userId");

            migrationBuilder.RenameIndex(
                name: "IX_checkoutTransactions_bookInventoryId",
                table: "CheckoutTransactions",
                newName: "IX_CheckoutTransactions_bookInventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_checkoutTransactions_UserId",
                table: "CheckoutTransactions",
                newName: "IX_CheckoutTransactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_checkoutTransactions_MemberId",
                table: "CheckoutTransactions",
                newName: "IX_CheckoutTransactions_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_GetNotifications_userId",
                table: "Notifications",
                newName: "IX_Notifications_userId");

            migrationBuilder.RenameIndex(
                name: "IX_GetNotifications_memberId",
                table: "Notifications",
                newName: "IX_Notifications_memberId");

            migrationBuilder.RenameIndex(
                name: "IX_GetNotifications_checkoutTransactionId",
                table: "Notifications",
                newName: "IX_Notifications_checkoutTransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "userId", "roleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberTypes",
                table: "MemberTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckoutTransactions",
                table: "CheckoutTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookInventories",
                table: "BookInventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2358));

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2359));

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2361));

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2361));

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2362));

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2363));

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2364));

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutTransactions_ApplicationUsers_UserId",
                table: "CheckoutTransactions",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutTransactions_BookInventories_bookInventoryId",
                table: "CheckoutTransactions",
                column: "bookInventoryId",
                principalTable: "BookInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutTransactions_Members_MemberId",
                table: "CheckoutTransactions",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_ApplicationUsers_userId",
                table: "Genres",
                column: "userId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_ApplicationUsers_userId",
                table: "Members",
                column: "userId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MemberTypes_MemberTypeId",
                table: "Members",
                column: "MemberTypeId",
                principalTable: "MemberTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_ApplicationUsers_userId",
                table: "Notifications",
                column: "userId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CheckoutTransactions_checkoutTransactionId",
                table: "Notifications",
                column: "checkoutTransactionId",
                principalTable: "CheckoutTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Members_memberId",
                table: "Notifications",
                column: "memberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_ApplicationUsers_userId",
                table: "UserRoles",
                column: "userId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_roleId",
                table: "UserRoles",
                column: "roleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutTransactions_ApplicationUsers_UserId",
                table: "CheckoutTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutTransactions_BookInventories_bookInventoryId",
                table: "CheckoutTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutTransactions_Members_MemberId",
                table: "CheckoutTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_ApplicationUsers_userId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_ApplicationUsers_userId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_MemberTypes_MemberTypeId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_ApplicationUsers_userId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CheckoutTransactions_checkoutTransactionId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Members_memberId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_ApplicationUsers_userId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_roleId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberTypes",
                table: "MemberTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckoutTransactions",
                table: "CheckoutTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookInventories",
                table: "BookInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "userRoles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "members");

            migrationBuilder.RenameTable(
                name: "MemberTypes",
                newName: "memberTypes");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "genres");

            migrationBuilder.RenameTable(
                name: "CheckoutTransactions",
                newName: "checkoutTransactions");

            migrationBuilder.RenameTable(
                name: "BookInventories",
                newName: "bookInventories");

            migrationBuilder.RenameTable(
                name: "ApplicationUsers",
                newName: "applicationUsers");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "GetNotifications");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_roleId",
                table: "userRoles",
                newName: "IX_userRoles_roleId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_userId",
                table: "members",
                newName: "IX_members_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_MemberTypeId",
                table: "members",
                newName: "IX_members_MemberTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Genres_userId",
                table: "genres",
                newName: "IX_genres_userId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckoutTransactions_bookInventoryId",
                table: "checkoutTransactions",
                newName: "IX_checkoutTransactions_bookInventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckoutTransactions_UserId",
                table: "checkoutTransactions",
                newName: "IX_checkoutTransactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckoutTransactions_MemberId",
                table: "checkoutTransactions",
                newName: "IX_checkoutTransactions_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_userId",
                table: "GetNotifications",
                newName: "IX_GetNotifications_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_memberId",
                table: "GetNotifications",
                newName: "IX_GetNotifications_memberId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_checkoutTransactionId",
                table: "GetNotifications",
                newName: "IX_GetNotifications_checkoutTransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles",
                columns: new[] { "userId", "roleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_members",
                table: "members",
                column: "MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_memberTypes",
                table: "memberTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_genres",
                table: "genres",
                column: "GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_checkoutTransactions",
                table: "checkoutTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookInventories",
                table: "bookInventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_applicationUsers",
                table: "applicationUsers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetNotifications",
                table: "GetNotifications",
                column: "NotificationId");

            migrationBuilder.CreateTable(
                name: "temp_data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Member_Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temp_data", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 11, 6, 48, 30, 454, DateTimeKind.Utc).AddTicks(8684));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 11, 6, 48, 30, 454, DateTimeKind.Utc).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 11, 6, 48, 30, 454, DateTimeKind.Utc).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 11, 6, 48, 30, 454, DateTimeKind.Utc).AddTicks(8688));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 11, 6, 48, 30, 454, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 11, 6, 48, 30, 454, DateTimeKind.Utc).AddTicks(8690));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 11, 6, 48, 30, 454, DateTimeKind.Utc).AddTicks(8691));

            migrationBuilder.AddForeignKey(
                name: "FK_GetNotifications_applicationUsers_userId",
                table: "GetNotifications",
                column: "userId",
                principalTable: "applicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GetNotifications_checkoutTransactions_checkoutTransactionId",
                table: "GetNotifications",
                column: "checkoutTransactionId",
                principalTable: "checkoutTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GetNotifications_members_memberId",
                table: "GetNotifications",
                column: "memberId",
                principalTable: "members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_checkoutTransactions_applicationUsers_UserId",
                table: "checkoutTransactions",
                column: "UserId",
                principalTable: "applicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_checkoutTransactions_bookInventories_bookInventoryId",
                table: "checkoutTransactions",
                column: "bookInventoryId",
                principalTable: "bookInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_checkoutTransactions_members_MemberId",
                table: "checkoutTransactions",
                column: "MemberId",
                principalTable: "members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_genres_applicationUsers_userId",
                table: "genres",
                column: "userId",
                principalTable: "applicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_members_applicationUsers_userId",
                table: "members",
                column: "userId",
                principalTable: "applicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_members_memberTypes_MemberTypeId",
                table: "members",
                column: "MemberTypeId",
                principalTable: "memberTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_applicationUsers_userId",
                table: "userRoles",
                column: "userId",
                principalTable: "applicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_roles_roleId",
                table: "userRoles",
                column: "roleId",
                principalTable: "roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
