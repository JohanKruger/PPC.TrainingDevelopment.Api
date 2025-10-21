using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface IEmployeeLookupService
    {
        Task<IEnumerable<EmployeeLookup>> GetAllAsync();
        Task<EmployeeLookup?> GetByPersonnelNumberAsync(string personnelNumber);
        Task<IEnumerable<EmployeeLookup>> SearchAsync(string searchTerm);
        Task<EmployeeLookup> CreateAsync(EmployeeLookup employeeLookup);
        Task<EmployeeLookup?> UpdateAsync(string personnelNumber, EmployeeLookup employeeLookup);
        Task<bool> DeleteAsync(string personnelNumber);
        Task<bool> ExistsAsync(string personnelNumber);
        Task<IEnumerable<EmployeeLookup>> GetByRaceAsync(string race);
        Task<IEnumerable<EmployeeLookup>> GetByGenderAsync(string gender);
        Task<IEnumerable<EmployeeLookup>> GetByEELevelAsync(string eeLevel);
        Task<IEnumerable<EmployeeLookup>> GetByEECategoryAsync(string eeCategory);
        Task<IEnumerable<EmployeeLookup>> GetWithDisabilityAsync(bool hasDisability);
    }
}