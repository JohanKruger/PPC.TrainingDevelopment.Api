using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPC.TrainingDevelopment.Api.Models
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditLogId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string HttpMethod { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string RequestPath { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? QueryString { get; set; }

        [Required]
        [MaxLength(100)]
        public string Controller { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        public string? RequestBody { get; set; }

        public string? ResponseBody { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public long DurationMs { get; set; }

        [MaxLength(45)]
        public string? IpAddress { get; set; }

        [MaxLength(500)]
        public string? UserAgent { get; set; }

        [MaxLength(2000)]
        public string? ExceptionDetails { get; set; }

        [MaxLength(500)]
        public string? AdditionalInfo { get; set; }
    }
}