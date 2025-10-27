using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task LogAsync(AuditLog auditLog);
        Task<IEnumerable<AuditLog>> GetAuditLogsAsync(int page = 1, int pageSize = 50);
        Task<IEnumerable<AuditLog>> GetAuditLogsByUserAsync(string userId, int page = 1, int pageSize = 50);
        Task<IEnumerable<AuditLog>> GetAuditLogsByDateRangeAsync(DateTime fromDate, DateTime toDate, int page = 1, int pageSize = 50);
        Task<IEnumerable<AuditLog>> GetAuditLogsByControllerAsync(string controller, int page = 1, int pageSize = 50);
        Task<AuditLog?> GetAuditLogByIdAsync(int auditLogId);
    }
}