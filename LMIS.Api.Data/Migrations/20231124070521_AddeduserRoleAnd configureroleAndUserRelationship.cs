using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMIS.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddeduserRoleAndconfigureroleAndUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userRoles",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    roleId = table.Column<int>(type: "integer", nullable: false),
                    userRoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRoles", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "FK_userRoles_applicationUsers_userId",
                        column: x => x.userId,
                        principalTable: "applicationUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userRoles_roles_roleId",
                        column: x => x.roleId,
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userRoles_roleId",
                table: "userRoles",
                column: "roleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userRoles");
        }
    }
}
