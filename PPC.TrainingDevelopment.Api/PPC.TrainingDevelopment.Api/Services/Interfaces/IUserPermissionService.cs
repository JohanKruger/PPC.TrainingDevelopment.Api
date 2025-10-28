using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface IUserPermissionService
    {
        Task<IEnumerable<UserPermission>> GetAllAsync();
        Task<UserPermission?> GetByIdAsync(int permissionId);
        Task<IEnumerable<UserPermission>> GetByUsernameAsync(string username);
        Task<IEnumerable<UserPermission>> GetByPermissionCodeAsync(string permissionCode);
        Task<UserPermission> CreateAsync(UserPermission userPermission);
        Task<UserPermission?> UpdateAsync(int permissionId, UserPermission userPermission);
        Task<bool> DeleteAsync(int permissionId);
        Task<bool> ExistsAsync(int permissionId);
        Task<bool> HasPermissionAsync(string username, string permissionCode);
        Task<IEnumerable<UserPermission>> SearchAsync(string searchTerm);
    }
}