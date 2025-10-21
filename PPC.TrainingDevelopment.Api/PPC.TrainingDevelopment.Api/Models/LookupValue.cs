using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PPC.TrainingDevelopment.Api.Models
{
    public class LookupValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LookupId { get; set; }

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

        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation property for hierarchical data
        [JsonIgnore]
        [ForeignKey("ParentId")]
        public virtual LookupValue? Parent { get; set; }

        [JsonIgnore]
        public virtual ICollection<LookupValue> Children { get; set; } = new List<LookupValue>();
    }
}