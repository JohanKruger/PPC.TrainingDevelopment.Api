using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Models.Request
{
    public class CreateLookupValueRequest
    {
        [Required]
        [MaxLength(50)]
        public string LookupType { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Value { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Code { get; set; }

        public int? ParentId { get; set; }

        public int? SortOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateLookupValueRequest
    {
        [Required]
        [MaxLength(50)]
        public string LookupType { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Value { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Code { get; set; }

        public int? ParentId { get; set; }

        public int? SortOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}