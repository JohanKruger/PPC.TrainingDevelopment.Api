using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class LookupValueService : ILookupValueService
    {
        private readonly TrainingDevelopmentDbContext _context;

        public LookupValueService(TrainingDevelopmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LookupValue>> GetAllAsync()
        {
            return await _context.LookupValues
                .Include(lv => lv.Parent)
                .Include(lv => lv.Children)
                .OrderBy(lv => lv.LookupType)
                .ThenBy(lv => lv.SortOrder)
                .ThenBy(lv => lv.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<LookupValue>> GetByTypeAsync(string lookupType)
        {
            return await _context.LookupValues
                .Include(lv => lv.Parent)
                .Include(lv => lv.Children)
                .Where(lv => lv.LookupType == lookupType)
                .OrderBy(lv => lv.SortOrder)
                .ThenBy(lv => lv.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<LookupValue>> GetActiveByTypeAsync(string lookupType)
        {
            return await _context.LookupValues
                .Include(lv => lv.Parent)
                .Include(lv => lv.Children)
                .Where(lv => lv.LookupType == lookupType && lv.IsActive)
                .OrderBy(lv => lv.SortOrder)
                .ThenBy(lv => lv.Value)
                .ToListAsync();
        }

        public async Task<LookupValue?> GetByIdAsync(int id)
        {
            return await _context.LookupValues
                .Include(lv => lv.Parent)
                .Include(lv => lv.Children)
                .FirstOrDefaultAsync(lv => lv.LookupId == id);
        }

        public async Task<IEnumerable<LookupValue>> GetChildrenAsync(int parentId)
        {
            return await _context.LookupValues
                .Include(lv => lv.Parent)
                .Include(lv => lv.Children)
                .Where(lv => lv.ParentId == parentId)
                .OrderBy(lv => lv.SortOrder)
                .ThenBy(lv => lv.Value)
                .ToListAsync();
        }

        public async Task<LookupValue> CreateAsync(LookupValue lookupValue)
        {
            _context.LookupValues.Add(lookupValue);
            await _context.SaveChangesAsync();
            return lookupValue;
        }

        public async Task<LookupValue?> UpdateAsync(int id, LookupValue lookupValue)
        {
            var existingLookupValue = await _context.LookupValues.FindAsync(id);
            if (existingLookupValue == null)
            {
                return null;
            }

            existingLookupValue.LookupType = lookupValue.LookupType;
            existingLookupValue.Value = lookupValue.Value;
            existingLookupValue.Code = lookupValue.Code;
            existingLookupValue.ParentId = lookupValue.ParentId;
            existingLookupValue.SortOrder = lookupValue.SortOrder;
            existingLookupValue.IsActive = lookupValue.IsActive;

            await _context.SaveChangesAsync();
            return existingLookupValue;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lookupValue = await _context.LookupValues.FindAsync(id);
            if (lookupValue == null)
            {
                return false;
            }

            // Check if this lookup value has children
            var hasChildren = await _context.LookupValues
                .AnyAsync(lv => lv.ParentId == id);

            if (hasChildren)
            {
                // Don't allow deletion if it has children
                // You could also implement cascade delete or set children's parent to null
                return false;
            }

            _context.LookupValues.Remove(lookupValue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.LookupValues.AnyAsync(lv => lv.LookupId == id);
        }
    }
}