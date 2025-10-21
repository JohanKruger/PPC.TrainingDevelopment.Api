using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByPersonnelNumberAsync(string personnelNumber);
        Task<IEnumerable<Employee>> SearchAsync(string searchTerm);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee?> UpdateAsync(string personnelNumber, Employee employee);
        Task<bool> DeleteAsync(string personnelNumber);
        Task<bool> ExistsAsync(string personnelNumber);
        Task<IEnumerable<Employee>> GetByRaceAsync(string race);
        Task<IEnumerable<Employee>> GetByGenderAsync(string gender);
        Task<IEnumerable<Employee>> GetByEELevelAsync(string eeLevel);
        Task<IEnumerable<Employee>> GetByEECategoryAsync(string eeCategory);
        Task<IEnumerable<Employee>> GetWithDisabilityAsync(bool hasDisability);
        Task<IEnumerable<Employee>> GetByJobTitleAsync(string jobTitle);
        Task<IEnumerable<Employee>> GetByJobGradeAsync(string jobGrade);
        Task<IEnumerable<Employee>> GetBySiteAsync(string site);
        Task<Employee?> GetByIDNumberAsync(string idNumber);
    }
}