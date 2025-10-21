using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PPC.TrainingDevelopment.Api.Models
{
    public class TrainingEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainingEventId { get; set; }

        [MaxLength(20)]
        public string? PersonnelNumber { get; set; }

        [MaxLength(13)]
        public string? IDNumber { get; set; }

        [Required]
        public int EventTypeId { get; set; }

        [Required]
        public int TrainingEventNameId { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        [Required]
        public int MunicipalityId { get; set; }

        [Required]
        public int SiteId { get; set; }

        // Navigation properties
        [ForeignKey("PersonnelNumber")]
        public virtual Employee? Employee { get; set; }

        [ForeignKey("IDNumber")]
        public virtual NonEmployee? NonEmployee { get; set; }

        // Lookup navigation properties (if you have these models)
        [ForeignKey("EventTypeId")]
        public virtual LookupValue? EventType { get; set; }

        [ForeignKey("TrainingEventNameId")]
        public virtual LookupValue? TrainingEventName { get; set; }

        [ForeignKey("RegionId")]
        public virtual LookupValue? Region { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual LookupValue? Province { get; set; }

        [ForeignKey("MunicipalityId")]
        public virtual LookupValue? Municipality { get; set; }

        [ForeignKey("SiteId")]
        public virtual LookupValue? Site { get; set; }

        // Collection navigation property
        public virtual ICollection<TrainingRecordEvent> TrainingRecordEvents { get; set; } = new List<TrainingRecordEvent>();
    }
}