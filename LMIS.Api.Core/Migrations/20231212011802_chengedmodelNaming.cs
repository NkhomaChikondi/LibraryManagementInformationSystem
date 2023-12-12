using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class chengedmodelNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutTransactions_BookInventories_bookInventoryId",
                table: "CheckoutTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_ApplicationUsers_userId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_ApplicationUsers_userId",
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

            migrationBuilder.RenameColumn(
                name: "userRoleId",
                table: "UserRoles",
                newName: "UserRoleId");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "UserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_roleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "memberId",
                table: "Notifications",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_userId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_memberId",
                table: "Notifications",
                newName: "IX_Notifications_MemberId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Members",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Member_Code",
                table: "Members",
                newName: "MemberCode");

            migrationBuilder.RenameColumn(
                name: "Last_Name",
                table: "Members",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "First_Name",
                table: "Members",
                newName: "FirstName");

            migrationBuilder.RenameIndex(
                name: "IX_Members_userId",
                table: "Members",
                newName: "IX_Members_UserId");

            migrationBuilder.RenameColumn(
                name: "isReturned",
                table: "CheckoutTransactions",
                newName: "IsReturned");

            migrationBuilder.RenameColumn(
                name: "bookInventoryId",
                table: "CheckoutTransactions",
                newName: "BookInventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckoutTransactions_bookInventoryId",
                table: "CheckoutTransactions",
                newName: "IX_CheckoutTransactions_BookInventoryId");

            migrationBuilder.RenameColumn(
                name: "isAvailable",
                table: "BookInventories",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "ApplicationUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "ApplicationUsers",
                newName: "FirstName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "UserRoles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "Members",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "MemberTypes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "CheckoutTransactions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 18, 2, 270, DateTimeKind.Utc).AddTicks(3516), null });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 18, 2, 270, DateTimeKind.Utc).AddTicks(3519), null });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 18, 2, 270, DateTimeKind.Utc).AddTicks(3520), null });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 18, 2, 270, DateTimeKind.Utc).AddTicks(3520), null });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 18, 2, 270, DateTimeKind.Utc).AddTicks(3521), null });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 18, 2, 270, DateTimeKind.Utc).AddTicks(3522), null });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 18, 2, 270, DateTimeKind.Utc).AddTicks(3523), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "DeletedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "DeletedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "DeletedDate",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutTransactions_BookInventories_BookInventoryId",
                table: "CheckoutTransactions",
                column: "BookInventoryId",
                principalTable: "BookInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_ApplicationUsers_UserId",
                table: "Members",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_ApplicationUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Members_MemberId",
                table: "Notifications",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_ApplicationUsers_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutTransactions_BookInventories_BookInventoryId",
                table: "CheckoutTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_ApplicationUsers_UserId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_ApplicationUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Members_MemberId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_ApplicationUsers_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                table: "UserRoles",
                newName: "userRoleId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRoles",
                newName: "roleId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRoles",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_roleId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notifications",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Notifications",
                newName: "memberId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_MemberId",
                table: "Notifications",
                newName: "IX_Notifications_memberId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Members",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "MemberCode",
                table: "Members",
                newName: "Member_Code");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Members",
                newName: "Last_Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Members",
                newName: "First_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Members_UserId",
                table: "Members",
                newName: "IX_Members_userId");

            migrationBuilder.RenameColumn(
                name: "IsReturned",
                table: "CheckoutTransactions",
                newName: "isReturned");

            migrationBuilder.RenameColumn(
                name: "BookInventoryId",
                table: "CheckoutTransactions",
                newName: "bookInventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckoutTransactions_BookInventoryId",
                table: "CheckoutTransactions",
                newName: "IX_CheckoutTransactions_bookInventoryId");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "BookInventories",
                newName: "isAvailable");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ApplicationUsers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "ApplicationUsers",
                newName: "firstName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "UserRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "Members",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "MemberTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "CheckoutTransactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2358), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2359), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2361), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2361), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2362), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2363), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "DeletedDate" },
                values: new object[] { new DateTime(2023, 12, 12, 1, 5, 37, 795, DateTimeKind.Utc).AddTicks(2364), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "DeletedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "DeletedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "DeletedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutTransactions_BookInventories_bookInventoryId",
                table: "CheckoutTransactions",
                column: "bookInventoryId",
                principalTable: "BookInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_ApplicationUsers_userId",
                table: "Members",
                column: "userId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_ApplicationUsers_userId",
                table: "Notifications",
                column: "userId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
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
    }
}
