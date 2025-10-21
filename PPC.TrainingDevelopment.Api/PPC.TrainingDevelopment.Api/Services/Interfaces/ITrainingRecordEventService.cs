using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface ITrainingRecordEventService
    {
        Task<IEnumerable<TrainingRecordEvent>> GetAllAsync();
        Task<TrainingRecordEvent?> GetByIdAsync(int trainingRecordEventId);
        Task<IEnumerable<TrainingRecordEvent>> GetByTrainingEventIdAsync(int trainingEventId);
        Task<IEnumerable<TrainingRecordEvent>> GetByPersonnelNumberAsync(string personnelNumber);
        Task<IEnumerable<TrainingRecordEvent>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TrainingRecordEvent>> GetWithEvidenceAsync();
        Task<IEnumerable<TrainingRecordEvent>> GetWithoutEvidenceAsync();
        Task<decimal> GetTotalCostsByTrainingEventIdAsync(int trainingEventId);
        Task<decimal> GetTotalCostsByPersonnelNumberAsync(string personnelNumber);
        Task<decimal> GetTotalCostsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<TrainingRecordEvent> CreateAsync(TrainingRecordEvent trainingRecordEvent);
        Task<TrainingRecordEvent?> UpdateAsync(int trainingRecordEventId, TrainingRecordEvent trainingRecordEvent);
        Task<bool> DeleteAsync(int trainingRecordEventId);
        Task<bool> ExistsAsync(int trainingRecordEventId);
        Task<bool> ValidateDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<bool> ValidateTrainingEventExistsAsync(int trainingEventId);
    }
}