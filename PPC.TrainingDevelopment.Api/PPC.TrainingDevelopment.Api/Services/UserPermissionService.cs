using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly TrainingDevelopmentDbContext _context;

        public UserPermissionService(TrainingDevelopmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserPermission>> GetAllAsync()
        {
            return await _context.UserPermissions
                .OrderBy(up => up.Username)
                .ThenBy(up => up.PermissionCode)
                .ToListAsync();
        }

        public async Task<UserPermission?> GetByIdAsync(int permissionId)
        {
            return await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.PermissionId == permissionId);
        }

        public async Task<IEnumerable<UserPermission>> GetByUsernameAsync(string username)
        {
            return await _context.UserPermissions
                .Where(up => up.Username == username)
                .OrderBy(up => up.PermissionCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserPermission>> GetByPermissionCodeAsync(string permissionCode)
        {
            return await _context.UserPermissions
                .Where(up => up.PermissionCode == permissionCode)
                .OrderBy(up => up.Username)
                .ToListAsync();
        }

        public async Task<UserPermission> CreateAsync(UserPermission userPermission)
        {
            userPermission.CreatedDate = DateTime.Now;
            _context.UserPermissions.Add(userPermission);
            await _context.SaveChangesAsync();

            // Return the created entity
            return await GetByIdAsync(userPermission.PermissionId) ?? userPermission;
        }

        public async Task<UserPermission?> UpdateAsync(int permissionId, UserPermission userPermission)
        {
            var existingPermission = await _context.UserPermissions.FindAsync(permissionId);
            if (existingPermission == null)
            {
                return null;
            }

            existingPermission.Username = userPermission.Username;
            existingPermission.PermissionCode = userPermission.PermissionCode;
            // Note: CreatedDate should not be updated

            _context.UserPermissions.Update(existingPermission);
            await _context.SaveChangesAsync();

            // Return the updated entity
            return await GetByIdAsync(permissionId);
        }

        public async Task<bool> DeleteAsync(int permissionId)
        {
            var userPermission = await _context.UserPermissions.FindAsync(permissionId);
            if (userPermission == null)
            {
                return false;
            }

            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int permissionId)
        {
            return await _context.UserPermissions.AnyAsync(up => up.PermissionId == permissionId);
        }

        public async Task<bool> HasPermissionAsync(string username, string permissionCode)
        {
            return await _context.UserPermissions
                .AnyAsync(up => up.Username == username && up.PermissionCode == permissionCode);
        }

        public async Task<IEnumerable<UserPermission>> SearchAsync(string searchTerm)
        {
            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.UserPermissions
                .Where(up => up.Username.ToLower().Contains(lowerSearchTerm) ||
                           up.PermissionCode.ToLower().Contains(lowerSearchTerm))
                .OrderBy(up => up.Username)
                .ThenBy(up => up.PermissionCode)
                .ToListAsync();
        }
    }
}