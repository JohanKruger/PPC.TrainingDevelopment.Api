using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPermissionsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PermissionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Employees_PersonnelNo",
                        column: x => x.PersonnelNo,
                        principalTable: "Employees",
                        principalColumn: "PersonnelNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_CreatedDate",
                table: "UserPermissions",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionCode",
                table: "UserPermissions",
                column: "PermissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PersonnelNo",
                table: "UserPermissions",
                column: "PersonnelNo");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PersonnelNo_PermissionCode",
                table: "UserPermissions",
                columns: new[] { "PersonnelNo", "PermissionCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPermissions");
        }
    }
}
