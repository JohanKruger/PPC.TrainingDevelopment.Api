using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingRecordEventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingRecordEvents",
                columns: table => new
                {
                    TrainingRecordEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingEventId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: true),
                    Minutes = table.Column<int>(type: "int", nullable: true),
                    PersonnelNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Evidence = table.Column<bool>(type: "bit", nullable: true),
                    CostTrainingMaterials = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostTrainers = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostTrainingFacilities = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ScholarshipsBursaries = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CourseFees = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AccommodationTravel = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdministrationCosts = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EquipmentDepreciation = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRecordEvents", x => x.TrainingRecordEventId);
                    table.ForeignKey(
                        name: "FK_TrainingRecordEvents_TrainingEvents_TrainingEventId",
                        column: x => x.TrainingEventId,
                        principalTable: "TrainingEvents",
                        principalColumn: "TrainingEventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRecordEvents_EndDate",
                table: "TrainingRecordEvents",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRecordEvents_Evidence",
                table: "TrainingRecordEvents",
                column: "Evidence");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRecordEvents_PersonnelNumber",
                table: "TrainingRecordEvents",
                column: "PersonnelNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRecordEvents_StartDate",
                table: "TrainingRecordEvents",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRecordEvents_TrainingEventId",
                table: "TrainingRecordEvents",
                column: "TrainingEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingRecordEvents");
        }
    }
}
