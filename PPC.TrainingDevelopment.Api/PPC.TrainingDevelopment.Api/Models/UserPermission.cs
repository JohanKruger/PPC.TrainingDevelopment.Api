using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPC.TrainingDevelopment.Api.Models
{
    public class UserPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }

        [Required]
        [MaxLength(20)]
        public string PersonnelNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string PermissionCode { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("PersonnelNo")]
        public virtual Employee? Employee { get; set; }
    }
}