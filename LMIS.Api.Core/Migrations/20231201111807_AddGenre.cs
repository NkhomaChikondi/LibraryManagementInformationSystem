using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Book",
                newName: "Author");

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "bookInventories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "bookInventories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isAvailable",
                table: "bookInventories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CopyNumber",
                table: "Book",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaximumBooksAllowed = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.GenreId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "bookInventories");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "bookInventories");

            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "bookInventories");

            migrationBuilder.DropColumn(
                name: "CopyNumber",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Book",
                newName: "Location");

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Book",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isAvailable",
                table: "Book",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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
        }
    }
}
