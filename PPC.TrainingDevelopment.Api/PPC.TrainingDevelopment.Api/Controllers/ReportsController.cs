using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPC.TrainingDevelopment.Api.Models.Response;
using PPC.TrainingDevelopment.Api.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PPC.TrainingDevelopment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IReportsService reportsService, ILogger<ReportsController> logger)
        {
            _reportsService = reportsService;
            _logger = logger;
        }

        /// <summary>
        /// Get all training records with related training event and employee data in flat structure for CSV export
        /// </summary>
        /// <returns>List of training records with flat structure</returns>
        [HttpGet("training-records/export")]
        public async Task<ActionResult<IEnumerable<TrainingReportResponse>>> GetTrainingRecordsForExport()
        {
            try
            {
                _logger.LogInformation("Getting all training records for export");
                var reports = await _reportsService.GetTrainingRecordsForExportAsync();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting training records for export");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        /// <summary>
        /// Get training records filtered by date range with related data in flat structure for CSV export
        /// </summary>
        /// <param name="startDate">Start date filter (yyyy-MM-dd)</param>
        /// <param name="endDate">End date filter (yyyy-MM-dd)</param>
        /// <returns>List of training records with flat structure</returns>
        [HttpGet("training-records/export/by-date")]
        public async Task<ActionResult<IEnumerable<TrainingReportResponse>>> GetTrainingRecordsByDateForExport(
            [Required] DateTime startDate,
            [Required] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    return BadRequest("Start date cannot be greater than end date");
                }

                _logger.LogInformation("Getting training records for export by date range: {StartDate} to {EndDate}", startDate, endDate);
                var reports = await _reportsService.GetTrainingRecordsForExportAsync(startDate, endDate);
                return Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting training records by date for export");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        /// <summary>
        /// Get training records filtered by personnel number with related data in flat structure for CSV export
        /// </summary>
        /// <param name="personnelNumber">Personnel number to filter by</param>
        /// <returns>List of training records with flat structure</returns>
        [HttpGet("training-records/export/by-personnel/{personnelNumber}")]
        public async Task<ActionResult<IEnumerable<TrainingReportResponse>>> GetTrainingRecordsByPersonnelForExport(
            [Required] string personnelNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(personnelNumber))
                {
                    return BadRequest("Personnel number cannot be empty");
                }

                _logger.LogInformation("Getting training records for export by personnel number: {PersonnelNumber}", personnelNumber);
                var reports = await _reportsService.GetTrainingRecordsForExportAsync(personnelNumber);
                return Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting training records by personnel number for export");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        /// <summary>
        /// Get training records filtered by training event ID with related data in flat structure for CSV export
        /// </summary>
        /// <param name="trainingEventId">Training event ID to filter by</param>
        /// <returns>List of training records with flat structure</returns>
        [HttpGet("training-records/export/by-event/{trainingEventId:int}")]
        public async Task<ActionResult<IEnumerable<TrainingReportResponse>>> GetTrainingRecordsByEventForExport(
            [Required] int trainingEventId)
        {
            try
            {
                if (trainingEventId <= 0)
                {
                    return BadRequest("Training event ID must be greater than 0");
                }

                _logger.LogInformation("Getting training records for export by training event ID: {TrainingEventId}", trainingEventId);
                var reports = await _reportsService.GetTrainingRecordsByEventForExportAsync(trainingEventId);
                return Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting training records by event for export");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        /// <summary>
        /// Get training records with multiple optional filters with related data in flat structure for CSV export
        /// </summary>
        /// <param name="startDate">Optional start date filter (yyyy-MM-dd)</param>
        /// <param name="endDate">Optional end date filter (yyyy-MM-dd)</param>
        /// <param name="personnelNumber">Optional personnel number filter</param>
        /// <param name="trainingEventId">Optional training event ID filter</param>
        /// <param name="hasEvidence">Optional evidence filter (true/false)</param>
        /// <returns>List of training records with flat structure</returns>
        [HttpGet("training-records/export/filtered")]
        public async Task<ActionResult<IEnumerable<TrainingReportResponse>>> GetTrainingRecordsFilteredForExport(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? personnelNumber = null,
            int? trainingEventId = null,
            bool? hasEvidence = null)
        {
            try
            {
                if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                {
                    return BadRequest("Start date cannot be greater than end date");
                }

                if (trainingEventId.HasValue && trainingEventId <= 0)
                {
                    return BadRequest("Training event ID must be greater than 0");
                }

                _logger.LogInformation("Getting filtered training records for export with filters - StartDate: {StartDate}, EndDate: {EndDate}, PersonnelNumber: {PersonnelNumber}, TrainingEventId: {TrainingEventId}, HasEvidence: {HasEvidence}",
                    startDate, endDate, personnelNumber, trainingEventId, hasEvidence);

                var reports = await _reportsService.GetTrainingRecordsForExportAsync(
                    startDate, endDate, personnelNumber, trainingEventId, hasEvidence);

                return Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting filtered training records for export");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        /// <summary>
        /// Get training records as CSV file for download
        /// </summary>
        /// <param name="startDate">Optional start date filter (yyyy-MM-dd)</param>
        /// <param name="endDate">Optional end date filter (yyyy-MM-dd)</param>
        /// <param name="personnelNumber">Optional personnel number filter</param>
        /// <param name="trainingEventId">Optional training event ID filter</param>
        /// <param name="hasEvidence">Optional evidence filter (true/false)</param>
        /// <returns>CSV file download</returns>
        [HttpGet("training-records/export/csv")]
        public async Task<ActionResult> GetTrainingRecordsCsvExport(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? personnelNumber = null,
            int? trainingEventId = null,
            bool? hasEvidence = null)
        {
            try
            {
                if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                {
                    return BadRequest("Start date cannot be greater than end date");
                }

                if (trainingEventId.HasValue && trainingEventId <= 0)
                {
                    return BadRequest("Training event ID must be greater than 0");
                }

                _logger.LogInformation("Generating CSV export for training records with filters - StartDate: {StartDate}, EndDate: {EndDate}, PersonnelNumber: {PersonnelNumber}, TrainingEventId: {TrainingEventId}, HasEvidence: {HasEvidence}",
                    startDate, endDate, personnelNumber, trainingEventId, hasEvidence);

                var reports = await _reportsService.GetTrainingRecordsForExportAsync(
                    startDate, endDate, personnelNumber, trainingEventId, hasEvidence);

                var csv = GenerateCsvContent(reports);
                var fileName = $"training_records_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating CSV export for training records");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        private static string GenerateCsvContent(IEnumerable<TrainingReportResponse> reports)
        {
            var csv = new System.Text.StringBuilder();

            // CSV Headers
            csv.AppendLine("TrainingRecordEventId,TrainingEventId,StartDate,EndDate,Hours,Minutes,Evidence,ServiceProviderExternal," +
                          "CostTrainingMaterials,CostTrainers,CostTrainingFacilities,ScholarshipsBursaries,CourseFees,Accommodation," +
                          "Travel,Meal,AdministrationCosts,EquipmentDepreciation,TotalCosts,TotalDurationMinutes," +
                          "EventType,TrainingEventName,Region,Province,Municipality,Site," +
                          "PersonnelNumber,EmployeeFirstName,EmployeeLastName,EmployeeKnownName,EmployeeInitials," +
                          "EmployeeRace,EmployeeGender,EmployeeDisability,EmployeeEELevel,EmployeeEECategory," +
                          "EmployeeJobTitle,EmployeeJobGrade,EmployeeIDNumber,EmployeeSite,EmployeeHighestQualification," +
                          "EmployeeNotes,NonEmployeeIDNumber");

            // CSV Data
            foreach (var report in reports)
            {
                csv.AppendLine($"{report.TrainingRecordEventId}," +
                              $"{report.TrainingEventId}," +
                              $"{report.StartDate:yyyy-MM-dd HH:mm:ss}," +
                              $"{report.EndDate:yyyy-MM-dd HH:mm:ss}," +
                              $"{report.Hours}," +
                              $"{report.Minutes}," +
                              $"{report.Evidence}," +
                              $"\"{EscapeCsvValue(report.ServiceProviderExternal)}\"," +
                              $"{report.CostTrainingMaterials}," +
                              $"{report.CostTrainers}," +
                              $"{report.CostTrainingFacilities}," +
                              $"{report.ScholarshipsBursaries}," +
                              $"{report.CourseFees}," +
                              $"{report.Accommodation}," +
                              $"{report.Travel}," +
                              $"{report.Meal}," +
                              $"{report.AdministrationCosts}," +
                              $"{report.EquipmentDepreciation}," +
                              $"{report.TotalCosts}," +
                              $"{report.TotalDurationMinutes}," +
                              $"\"{EscapeCsvValue(report.EventType)}\"," +
                              $"\"{EscapeCsvValue(report.TrainingEventName)}\"," +
                              $"\"{EscapeCsvValue(report.Region)}\"," +
                              $"\"{EscapeCsvValue(report.Province)}\"," +
                              $"\"{EscapeCsvValue(report.Municipality)}\"," +
                              $"\"{EscapeCsvValue(report.Site)}\"," +
                              $"\"{EscapeCsvValue(report.PersonnelNumber)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeFirstName)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeLastName)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeKnownName)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeInitials)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeRace)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeGender)}\"," +
                              $"{report.EmployeeDisability}," +
                              $"\"{EscapeCsvValue(report.EmployeeEELevel)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeEECategory)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeJobTitle)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeJobGrade)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeIDNumber)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeSite)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeHighestQualification)}\"," +
                              $"\"{EscapeCsvValue(report.EmployeeNotes)}\"," +
                              $"\"{EscapeCsvValue(report.NonEmployeeIDNumber)}\"");
            }

            return csv.ToString();
        }

        private static string EscapeCsvValue(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            // Escape double quotes by doubling them
            return value.Replace("\"", "\"\"");
        }
    }
}