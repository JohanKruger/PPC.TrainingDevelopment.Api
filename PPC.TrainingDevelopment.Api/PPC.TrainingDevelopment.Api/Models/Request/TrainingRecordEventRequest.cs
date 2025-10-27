using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Models.Request
{
    public class CreateTrainingRecordEventRequest
    {
        [Required]
        public int TrainingEventId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? Hours { get; set; }

        public int? Minutes { get; set; }

        [MaxLength(20)]
        public string? PersonnelNumber { get; set; }

        public bool? Evidence { get; set; }

        [MaxLength(255)]
        public string? ServiceProviderExternal { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? CostTrainingMaterials { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? CostTrainers { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? CostTrainingFacilities { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? ScholarshipsBursaries { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? CourseFees { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? Accommodation { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? Travel { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? AdministrationCosts { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? EquipmentDepreciation { get; set; }
    }

    public class UpdateTrainingRecordEventRequest
    {
        [Required]
        public int TrainingEventId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? Hours { get; set; }

        public int? Minutes { get; set; }

        [MaxLength(20)]
        public string? PersonnelNumber { get; set; }

        public bool? Evidence { get; set; }

        [MaxLength(255)]
        public string? ServiceProviderExternal { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? CostTrainingMaterials { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? CostTrainers { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? CostTrainingFacilities { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? ScholarshipsBursaries { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? CourseFees { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? Accommodation { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? Travel { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? AdministrationCosts { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        public decimal? EquipmentDepreciation { get; set; }
    }
}