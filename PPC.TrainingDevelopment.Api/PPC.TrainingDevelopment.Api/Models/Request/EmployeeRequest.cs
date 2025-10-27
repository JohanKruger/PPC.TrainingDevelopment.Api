using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Models.Request
{
    public class CreateEmployeeRequest
    {
        [Required]
        [MaxLength(20)]
        public string PersonnelNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? KnownName { get; set; }

        [MaxLength(10)]
        public string? Initials { get; set; }

        [MaxLength(50)]
        public string? Race { get; set; }

        [MaxLength(20)]
        public string? Gender { get; set; }

        public bool? Disability { get; set; }

        [MaxLength(50)]
        public string? EELevel { get; set; }

        [MaxLength(50)]
        public string? EECategory { get; set; }

        [MaxLength(100)]
        public string? JobTitle { get; set; }

        [MaxLength(20)]
        public string? JobGrade { get; set; }

        [MaxLength(13)]
        public string? IDNumber { get; set; }

        [MaxLength(100)]
        public string? Site { get; set; }

        [MaxLength(100)]
        public string? HighestQualification { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }
    }

    public class UpdateEmployeeRequest
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? KnownName { get; set; }

        [MaxLength(10)]
        public string? Initials { get; set; }

        [MaxLength(50)]
        public string? Race { get; set; }

        [MaxLength(20)]
        public string? Gender { get; set; }

        public bool? Disability { get; set; }

        [MaxLength(50)]
        public string? EELevel { get; set; }

        [MaxLength(50)]
        public string? EECategory { get; set; }

        [MaxLength(100)]
        public string? JobTitle { get; set; }

        [MaxLength(20)]
        public string? JobGrade { get; set; }

        [MaxLength(13)]
        public string? IDNumber { get; set; }

        [MaxLength(100)]
        public string? Site { get; set; }

        [MaxLength(100)]
        public string? HighestQualification { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }
    }
}