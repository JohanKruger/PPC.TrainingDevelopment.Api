using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeLookupEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeLookups",
                columns: table => new
                {
                    PersonnelNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KnownName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Initials = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Race = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Disability = table.Column<bool>(type: "bit", nullable: true),
                    EELevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EECategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLookups", x => x.PersonnelNumber);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLookups_Disability",
                table: "EmployeeLookups",
                column: "Disability");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLookups_EECategory",
                table: "EmployeeLookups",
                column: "EECategory");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLookups_EELevel",
                table: "EmployeeLookups",
                column: "EELevel");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLookups_Gender",
                table: "EmployeeLookups",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLookups_LastName",
                table: "EmployeeLookups",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLookups_Race",
                table: "EmployeeLookups",
                column: "Race");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLookups");
        }
    }
}
