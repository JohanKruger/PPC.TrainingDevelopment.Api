using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPC.TrainingDevelopment.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrainingEventToUseLookupForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrainingEvents_EventType",
                table: "TrainingEvents");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "TrainingEvents");

            migrationBuilder.DropColumn(
                name: "TrainingEventName",
                table: "TrainingEvents");

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId",
                table: "TrainingEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainingEventNameId",
                table: "TrainingEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_EventTypeId",
                table: "TrainingEvents",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_TrainingEventNameId",
                table: "TrainingEvents",
                column: "TrainingEventNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingEvents_LookupValues_EventTypeId",
                table: "TrainingEvents",
                column: "EventTypeId",
                principalTable: "LookupValues",
                principalColumn: "LookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingEvents_LookupValues_TrainingEventNameId",
                table: "TrainingEvents",
                column: "TrainingEventNameId",
                principalTable: "LookupValues",
                principalColumn: "LookupId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingEvents_LookupValues_EventTypeId",
                table: "TrainingEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingEvents_LookupValues_TrainingEventNameId",
                table: "TrainingEvents");

            migrationBuilder.DropIndex(
                name: "IX_TrainingEvents_EventTypeId",
                table: "TrainingEvents");

            migrationBuilder.DropIndex(
                name: "IX_TrainingEvents_TrainingEventNameId",
                table: "TrainingEvents");

            migrationBuilder.DropColumn(
                name: "EventTypeId",
                table: "TrainingEvents");

            migrationBuilder.DropColumn(
                name: "TrainingEventNameId",
                table: "TrainingEvents");

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "TrainingEvents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrainingEventName",
                table: "TrainingEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_EventType",
                table: "TrainingEvents",
                column: "EventType");
        }
    }
}
