using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface ILookupValueService
    {
        Task<IEnumerable<LookupValue>> GetAllAsync();
        Task<IEnumerable<LookupValue>> GetByTypeAsync(string lookupType);
        Task<LookupValue?> GetByIdAsync(int id);
        Task<LookupValue> CreateAsync(LookupValue lookupValue);
        Task<LookupValue?> UpdateAsync(int id, LookupValue lookupValue);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<LookupValue>> GetActiveByTypeAsync(string lookupType);
        Task<IEnumerable<LookupValue>> GetChildrenAsync(int parentId);
        Task<bool> ExistsAsync(int id);
    }
}