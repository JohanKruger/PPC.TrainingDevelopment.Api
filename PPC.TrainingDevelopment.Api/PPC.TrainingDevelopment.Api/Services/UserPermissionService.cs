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
                .Include(up => up.Employee)
                .OrderBy(up => up.PersonnelNo)
                .ThenBy(up => up.PermissionCode)
                .ToListAsync();
        }

        public async Task<UserPermission?> GetByIdAsync(int permissionId)
        {
            return await _context.UserPermissions
                .Include(up => up.Employee)
                .FirstOrDefaultAsync(up => up.PermissionId == permissionId);
        }

        public async Task<IEnumerable<UserPermission>> GetByPersonnelNoAsync(string personnelNo)
        {
            return await _context.UserPermissions
                .Include(up => up.Employee)
                .Where(up => up.PersonnelNo == personnelNo)
                .OrderBy(up => up.PermissionCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserPermission>> GetByPermissionCodeAsync(string permissionCode)
        {
            return await _context.UserPermissions
                .Include(up => up.Employee)
                .Where(up => up.PermissionCode == permissionCode)
                .OrderBy(up => up.PersonnelNo)
                .ToListAsync();
        }

        public async Task<UserPermission> CreateAsync(UserPermission userPermission)
        {
            userPermission.CreatedDate = DateTime.Now;
            _context.UserPermissions.Add(userPermission);
            await _context.SaveChangesAsync();

            // Return the created entity with included Employee data
            return await GetByIdAsync(userPermission.PermissionId) ?? userPermission;
        }

        public async Task<UserPermission?> UpdateAsync(int permissionId, UserPermission userPermission)
        {
            var existingPermission = await _context.UserPermissions.FindAsync(permissionId);
            if (existingPermission == null)
            {
                return null;
            }

            existingPermission.PersonnelNo = userPermission.PersonnelNo;
            existingPermission.PermissionCode = userPermission.PermissionCode;
            // Note: CreatedDate should not be updated

            _context.UserPermissions.Update(existingPermission);
            await _context.SaveChangesAsync();

            // Return the updated entity with included Employee data
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

        public async Task<bool> HasPermissionAsync(string personnelNo, string permissionCode)
        {
            return await _context.UserPermissions
                .AnyAsync(up => up.PersonnelNo == personnelNo && up.PermissionCode == permissionCode);
        }

        public async Task<IEnumerable<UserPermission>> SearchAsync(string searchTerm)
        {
            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.UserPermissions
                .Include(up => up.Employee)
                .Where(up => up.PersonnelNo.ToLower().Contains(lowerSearchTerm) ||
                           up.PermissionCode.ToLower().Contains(lowerSearchTerm) ||
                           (up.Employee != null &&
                            (up.Employee.FirstName.ToLower().Contains(lowerSearchTerm) ||
                             up.Employee.LastName.ToLower().Contains(lowerSearchTerm))))
                .OrderBy(up => up.PersonnelNo)
                .ThenBy(up => up.PermissionCode)
                .ToListAsync();
        }
    }
}