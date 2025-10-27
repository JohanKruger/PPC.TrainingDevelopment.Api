using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogsController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        /// <summary>
        /// Get all audit log entries with pagination
        /// </summary>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 50, max: 100)</param>
        /// <returns>List of audit log entries</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogs(int page = 1, int pageSize = 50)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 50;

                var auditLogs = await _auditLogService.GetAuditLogsAsync(page, pageSize);
                return Ok(auditLogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving audit logs.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get audit log entry by ID
        /// </summary>
        /// <param name="id">Audit log ID</param>
        /// <returns>Audit log entry</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AuditLog>> GetAuditLog(int id)
        {
            try
            {
                var auditLog = await _auditLogService.GetAuditLogByIdAsync(id);
                if (auditLog == null)
                {
                    return NotFound(new { message = $"Audit log with ID {id} not found." });
                }
                return Ok(auditLog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the audit log.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get audit logs by user ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 50, max: 100)</param>
        /// <returns>List of audit log entries for the specified user</returns>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogsByUser(string userId, int page = 1, int pageSize = 50)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 50;

                var auditLogs = await _auditLogService.GetAuditLogsByUserAsync(userId, page, pageSize);
                return Ok(auditLogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving audit logs by user.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get audit logs by controller name
        /// </summary>
        /// <param name="controller">Controller name</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 50, max: 100)</param>
        /// <returns>List of audit log entries for the specified controller</returns>
        [HttpGet("controller/{controller}")]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogsByController(string controller, int page = 1, int pageSize = 50)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 50;

                var auditLogs = await _auditLogService.GetAuditLogsByControllerAsync(controller, page, pageSize);
                return Ok(auditLogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving audit logs by controller.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get audit logs by date range
        /// </summary>
        /// <param name="fromDate">Start date (ISO format)</param>
        /// <param name="toDate">End date (ISO format)</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 50, max: 100)</param>
        /// <returns>List of audit log entries within the specified date range</returns>
        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogsByDateRange(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            int page = 1,
            int pageSize = 50)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 50;

                if (fromDate > toDate)
                {
                    return BadRequest(new { message = "From date cannot be greater than to date." });
                }

                var auditLogs = await _auditLogService.GetAuditLogsByDateRangeAsync(fromDate, toDate, page, pageSize);
                return Ok(auditLogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving audit logs by date range.", error = ex.Message });
            }
        }
    }
}