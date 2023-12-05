using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeleteFieldsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "membersGenres");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "members",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "members",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "memberTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "memberTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "genres",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "genres",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "checkoutTransactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "checkoutTransactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "bookInventories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "bookInventories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "applicationUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "applicationUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "GetNotifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GetNotifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Book",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Book",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "DeletedDate", "IsDeleted" },
                values: new object[] { new DateTime(2023, 12, 5, 7, 41, 10, 751, DateTimeKind.Utc).AddTicks(5259), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "DeletedDate", "IsDeleted" },
                values: new object[] { new DateTime(2023, 12, 5, 7, 41, 10, 751, DateTimeKind.Utc).AddTicks(5261), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "DeletedDate", "IsDeleted" },
                values: new object[] { new DateTime(2023, 12, 5, 7, 41, 10, 751, DateTimeKind.Utc).AddTicks(5262), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "DeletedDate", "IsDeleted" },
                values: new object[] { new DateTime(2023, 12, 5, 7, 41, 10, 751, DateTimeKind.Utc).AddTicks(5262), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "DeletedDate", "IsDeleted" },
                values: new object[] { new DateTime(2023, 12, 5, 7, 41, 10, 751, DateTimeKind.Utc).AddTicks(5263), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedOn", "DeletedDate", "IsDeleted" },
                values: new object[] { new DateTime(2023, 12, 5, 7, 41, 10, 751, DateTimeKind.Utc).AddTicks(5264), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedOn", "DeletedDate", "IsDeleted" },
                values: new object[] { new DateTime(2023, 12, 5, 7, 41, 10, 751, DateTimeKind.Utc).AddTicks(5265), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "members");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "members");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "memberTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "memberTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "checkoutTransactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "checkoutTransactions");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "bookInventories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "bookInventories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "applicationUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "applicationUsers");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "GetNotifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GetNotifications");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Book");

            migrationBuilder.CreateTable(
                name: "membersGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Counter = table.Column<int>(type: "integer", nullable: false),
                    GenreName = table.Column<string>(type: "text", nullable: false),
                    memberCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membersGenres", x => x.Id);
                });

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
    }
}
