using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMIS.Api.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddednewmodelforbookManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    ObtainedThrough = table.Column<string>(type: "text", nullable: false),
                    Publisher = table.Column<string>(type: "text", nullable: false),
                    isAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    Genre = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bookInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bookInventories_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkoutTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CheckOutDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    BookInventoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkoutTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkoutTransactions_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkoutTransactions_applicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "applicationUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkoutTransactions_bookInventories_BookInventoryId",
                        column: x => x.BookInventoryId,
                        principalTable: "bookInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkoutTransactions_members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9060));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9062));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9063));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9064));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9065));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9066));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 30, 14, 1, 23, 735, DateTimeKind.Utc).AddTicks(9066));

            migrationBuilder.CreateIndex(
                name: "IX_bookInventories_BookId",
                table: "bookInventories",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_checkoutTransactions_BookId",
                table: "checkoutTransactions",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_checkoutTransactions_BookInventoryId",
                table: "checkoutTransactions",
                column: "BookInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_checkoutTransactions_MemberId",
                table: "checkoutTransactions",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_checkoutTransactions_UserId",
                table: "checkoutTransactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkoutTransactions");

            migrationBuilder.DropTable(
                name: "bookInventories");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 9, 17, 46, 736, DateTimeKind.Utc).AddTicks(5654));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 9, 17, 46, 736, DateTimeKind.Utc).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 9, 17, 46, 736, DateTimeKind.Utc).AddTicks(5657));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 9, 17, 46, 736, DateTimeKind.Utc).AddTicks(5657));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 9, 17, 46, 736, DateTimeKind.Utc).AddTicks(5658));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 9, 17, 46, 736, DateTimeKind.Utc).AddTicks(5659));

            migrationBuilder.UpdateData(
                table: "memberTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 29, 9, 17, 46, 736, DateTimeKind.Utc).AddTicks(5660));
        }
    }
}
