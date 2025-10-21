using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingEventAndNonEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NonEmployees",
                columns: table => new
                {
                    IDNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonEmployees", x => x.IDNumber);
                });

            migrationBuilder.CreateTable(
                name: "TrainingEvents",
                columns: table => new
                {
                    TrainingEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrainingEventName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    MunicipalityId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEvents", x => x.TrainingEventId);
                    table.ForeignKey(
                        name: "FK_TrainingEvents_Employees_PersonnelNumber",
                        column: x => x.PersonnelNumber,
                        principalTable: "Employees",
                        principalColumn: "PersonnelNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingEvents_LookupValues_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "LookupValues",
                        principalColumn: "LookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingEvents_LookupValues_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "LookupValues",
                        principalColumn: "LookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingEvents_LookupValues_RegionId",
                        column: x => x.RegionId,
                        principalTable: "LookupValues",
                        principalColumn: "LookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingEvents_LookupValues_SiteId",
                        column: x => x.SiteId,
                        principalTable: "LookupValues",
                        principalColumn: "LookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingEvents_NonEmployees_IDNumber",
                        column: x => x.IDNumber,
                        principalTable: "NonEmployees",
                        principalColumn: "IDNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_EventType",
                table: "TrainingEvents",
                column: "EventType");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_IDNumber",
                table: "TrainingEvents",
                column: "IDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_MunicipalityId",
                table: "TrainingEvents",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_PersonnelNumber",
                table: "TrainingEvents",
                column: "PersonnelNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_ProvinceId",
                table: "TrainingEvents",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_RegionId",
                table: "TrainingEvents",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_SiteId",
                table: "TrainingEvents",
                column: "SiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingEvents");

            migrationBuilder.DropTable(
                name: "NonEmployees");
        }
    }
}
