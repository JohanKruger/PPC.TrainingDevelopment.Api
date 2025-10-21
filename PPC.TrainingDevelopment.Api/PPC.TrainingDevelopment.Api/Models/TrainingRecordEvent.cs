using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PPC.TrainingDevelopment.Api.Models
{
    public class TrainingRecordEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainingRecordEventId { get; set; }

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

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostTrainingMaterials { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostTrainers { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostTrainingFacilities { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ScholarshipsBursaries { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CourseFees { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AccommodationTravel { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AdministrationCosts { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? EquipmentDepreciation { get; set; }

        // Navigation property
        [ForeignKey("TrainingEventId")]
        [JsonIgnore]
        public virtual TrainingEvent? TrainingEvent { get; set; }

        // Calculated property for total duration in minutes
        [NotMapped]
        public int? TotalDurationMinutes =>
            (Hours ?? 0) * 60 + (Minutes ?? 0);

        // Calculated property for total costs
        [NotMapped]
        public decimal TotalCosts =>
            (CostTrainingMaterials ?? 0) +
            (CostTrainers ?? 0) +
            (CostTrainingFacilities ?? 0) +
            (ScholarshipsBursaries ?? 0) +
            (CourseFees ?? 0) +
            (AccommodationTravel ?? 0) +
            (AdministrationCosts ?? 0) +
            (EquipmentDepreciation ?? 0);
    }
}