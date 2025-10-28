using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserPermissionToUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Employees_PersonnelNo",
                table: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_PersonnelNo",
                table: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_PersonnelNo_PermissionCode",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "PersonnelNo",
                table: "UserPermissions");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserPermissions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_Username",
                table: "UserPermissions",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_Username_PermissionCode",
                table: "UserPermissions",
                columns: new[] { "Username", "PermissionCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_Username",
                table: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_Username_PermissionCode",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserPermissions");

            migrationBuilder.AddColumn<string>(
                name: "PersonnelNo",
                table: "UserPermissions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PersonnelNo",
                table: "UserPermissions",
                column: "PersonnelNo");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PersonnelNo_PermissionCode",
                table: "UserPermissions",
                columns: new[] { "PersonnelNo", "PermissionCode" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Employees_PersonnelNo",
                table: "UserPermissions",
                column: "PersonnelNo",
                principalTable: "Employees",
                principalColumn: "PersonnelNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
