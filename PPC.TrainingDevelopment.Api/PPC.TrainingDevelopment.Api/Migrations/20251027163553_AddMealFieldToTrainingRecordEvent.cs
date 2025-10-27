using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMealFieldToTrainingRecordEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Meal",
                table: "TrainingRecordEvents",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meal",
                table: "TrainingRecordEvents");
        }
    }
}
