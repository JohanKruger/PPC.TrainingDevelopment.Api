using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly TrainingDevelopmentDbContext _context;
        private readonly ILogger<AuditLogService> _logger;

        public AuditLogService(TrainingDevelopmentDbContext context, ILogger<AuditLogService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task LogAsync(AuditLog auditLog)
        {
            try
            {
                _context.AuditLogs.Add(auditLog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save audit log entry");
                // Don't throw - we don't want audit logging to break the main application flow
            }
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync(int page = 1, int pageSize = 50)
        {
            return await _context.AuditLogs
                .OrderByDescending(al => al.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsByUserAsync(string userId, int page = 1, int pageSize = 50)
        {
            return await _context.AuditLogs
                .Where(al => al.UserId == userId)
                .OrderByDescending(al => al.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsByDateRangeAsync(DateTime fromDate, DateTime toDate, int page = 1, int pageSize = 50)
        {
            return await _context.AuditLogs
                .Where(al => al.Timestamp >= fromDate && al.Timestamp <= toDate)
                .OrderByDescending(al => al.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsByControllerAsync(string controller, int page = 1, int pageSize = 50)
        {
            return await _context.AuditLogs
                .Where(al => al.Controller == controller)
                .OrderByDescending(al => al.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<AuditLog?> GetAuditLogByIdAsync(int auditLogId)
        {
            return await _context.AuditLogs
                .FirstOrDefaultAsync(al => al.AuditLogId == auditLogId);
        }
    }
}