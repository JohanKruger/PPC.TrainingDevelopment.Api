using PPC.TrainingDevelopment.Api.Models.Response;

namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface IReportsService
    {
        /// <summary>
        /// Get all training record events with their related training event and employee data in a flat structure
        /// </summary>
        /// <returns>List of training records with flat structure</returns>
        Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync();

        /// <summary>
        /// Get training record events filtered by date range with related data in flat structure
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <returns>List of training records with flat structure</returns>
        Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Get training record events filtered by personnel number with related data in flat structure
        /// </summary>
        /// <param name="personnelNumber">Personnel number to filter by</param>
        /// <returns>List of training records with flat structure</returns>
        Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(string personnelNumber);

        /// <summary>
        /// Get training record events filtered by training event ID with related data in flat structure
        /// </summary>
        /// <param name="trainingEventId">Training event ID to filter by</param>
        /// <returns>List of training records with flat structure</returns>
        Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsByEventForExportAsync(int trainingEventId);

        /// <summary>
        /// Get training record events with multiple filters with related data in flat structure
        /// </summary>
        /// <param name="startDate">Optional start date filter</param>
        /// <param name="endDate">Optional end date filter</param>
        /// <param name="personnelNumber">Optional personnel number filter</param>
        /// <param name="trainingEventId">Optional training event ID filter</param>
        /// <param name="hasEvidence">Optional evidence filter</param>
        /// <returns>List of training records with flat structure</returns>
        Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? personnelNumber = null,
            int? trainingEventId = null,
            bool? hasEvidence = null);
    }
}