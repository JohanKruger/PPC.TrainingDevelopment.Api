using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Models.Request
{
    public class CreateUserPermissionRequest
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string PermissionCode { get; set; } = string.Empty;
    }

    public class UpdateUserPermissionRequest
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string PermissionCode { get; set; } = string.Empty;
    }
}