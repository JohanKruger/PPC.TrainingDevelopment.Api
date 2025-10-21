using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface ITrainingEventService
    {
        Task<IEnumerable<TrainingEvent>> GetAllAsync();
        Task<TrainingEvent?> GetByIdAsync(int trainingEventId);
        Task<IEnumerable<TrainingEvent>> GetByPersonnelNumberAsync(string personnelNumber);
        Task<IEnumerable<TrainingEvent>> GetByIDNumberAsync(string idNumber);
        Task<IEnumerable<TrainingEvent>> GetByEventTypeAsync(int eventTypeId);
        Task<IEnumerable<TrainingEvent>> GetByRegionAsync(int regionId);
        Task<IEnumerable<TrainingEvent>> GetByProvinceAsync(int provinceId);
        Task<IEnumerable<TrainingEvent>> GetByMunicipalityAsync(int municipalityId);
        Task<IEnumerable<TrainingEvent>> GetBySiteAsync(int siteId);
        Task<IEnumerable<TrainingEvent>> SearchByTrainingEventNameAsync(string searchTerm);
        Task<TrainingEvent> CreateAsync(TrainingEvent trainingEvent);
        Task<TrainingEvent?> UpdateAsync(int trainingEventId, TrainingEvent trainingEvent);
        Task<bool> DeleteAsync(int trainingEventId);
        Task<bool> ExistsAsync(int trainingEventId);
        Task<bool> ValidateParticipantAsync(string? personnelNumber, string? idNumber);
    }
}