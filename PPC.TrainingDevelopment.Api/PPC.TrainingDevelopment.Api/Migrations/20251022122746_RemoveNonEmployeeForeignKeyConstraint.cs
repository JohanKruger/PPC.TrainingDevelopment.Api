using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNonEmployeeForeignKeyConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingEvents_NonEmployees_IDNumber",
                table: "TrainingEvents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_TrainingEvents_NonEmployees_IDNumber",
                table: "TrainingEvents",
                column: "IDNumber",
                principalTable: "NonEmployees",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
