using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Models.Request
{
    public class CreateTrainingEventRequest
    {
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
    }

    public class UpdateTrainingEventRequest
    {
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
    }
}