using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Models
{
    public class NonEmployee
    {
        [Key]
        [Required]
        [MaxLength(13)]
        public string IDNumber { get; set; } = string.Empty;
    }
}