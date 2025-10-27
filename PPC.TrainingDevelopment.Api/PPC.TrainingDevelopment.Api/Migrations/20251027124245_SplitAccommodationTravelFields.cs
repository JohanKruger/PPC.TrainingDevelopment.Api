using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class SplitAccommodationTravelFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccommodationTravel",
                table: "TrainingRecordEvents",
                newName: "Travel");

            migrationBuilder.AddColumn<decimal>(
                name: "Accommodation",
                table: "TrainingRecordEvents",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accommodation",
                table: "TrainingRecordEvents");

            migrationBuilder.RenameColumn(
                name: "Travel",
                table: "TrainingRecordEvents",
                newName: "AccommodationTravel");
        }
    }
}
