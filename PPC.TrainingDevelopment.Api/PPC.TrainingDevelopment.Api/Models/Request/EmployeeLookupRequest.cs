using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Models.Request
{
    public class CreateEmployeeLookupRequest
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
    }

    public class UpdateEmployeeLookupRequest
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
    }
}