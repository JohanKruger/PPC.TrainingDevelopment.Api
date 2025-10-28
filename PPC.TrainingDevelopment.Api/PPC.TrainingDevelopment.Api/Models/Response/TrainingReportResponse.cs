using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Models.Response
{
    public class TrainingReportResponse
    {
        // Training Record Event fields
        public int TrainingRecordEventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public bool? Evidence { get; set; }
        public string? ServiceProviderExternal { get; set; }
        public decimal? CostTrainingMaterials { get; set; }
        public decimal? CostTrainers { get; set; }
        public decimal? CostTrainingFacilities { get; set; }
        public decimal? ScholarshipsBursaries { get; set; }
        public decimal? CourseFees { get; set; }
        public decimal? Accommodation { get; set; }
        public decimal? Travel { get; set; }
        public decimal? Meal { get; set; }
        public decimal? AdministrationCosts { get; set; }
        public decimal? EquipmentDepreciation { get; set; }
        public decimal TotalCosts { get; set; }
        public int? TotalDurationMinutes { get; set; }

        // Training Event fields
        public int TrainingEventId { get; set; }
        public string? EventType { get; set; }
        public string? TrainingEventName { get; set; }
        public string? Region { get; set; }
        public string? Province { get; set; }
        public string? Municipality { get; set; }
        public string? Site { get; set; }

        // Employee fields
        public string? PersonnelNumber { get; set; }
        public string? EmployeeFirstName { get; set; }
        public string? EmployeeLastName { get; set; }
        public string? EmployeeKnownName { get; set; }
        public string? EmployeeInitials { get; set; }
        public string? EmployeeRace { get; set; }
        public string? EmployeeGender { get; set; }
        public bool? EmployeeDisability { get; set; }
        public string? EmployeeEELevel { get; set; }
        public string? EmployeeEECategory { get; set; }
        public string? EmployeeJobTitle { get; set; }
        public string? EmployeeJobGrade { get; set; }
        public string? EmployeeIDNumber { get; set; }
        public string? EmployeeSite { get; set; }
        public string? EmployeeHighestQualification { get; set; }
        public string? EmployeeNotes { get; set; }

        // Non-Employee fields (for records where there's no employee match)
        public string? NonEmployeeIDNumber { get; set; }
    }
}