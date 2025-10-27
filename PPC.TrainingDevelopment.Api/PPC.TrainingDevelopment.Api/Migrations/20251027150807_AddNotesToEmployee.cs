using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Employees",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Employees");
        }
    }
}
