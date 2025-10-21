using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class EmployeeLookupService : IEmployeeLookupService
    {
        private readonly TrainingDevelopmentDbContext _context;

        public EmployeeLookupService(TrainingDevelopmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeLookup>> GetAllAsync()
        {
            return await _context.EmployeeLookups
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<EmployeeLookup?> GetByPersonnelNumberAsync(string personnelNumber)
        {
            return await _context.EmployeeLookups
                .FirstOrDefaultAsync(e => e.PersonnelNumber == personnelNumber);
        }

        public async Task<IEnumerable<EmployeeLookup>> SearchAsync(string searchTerm)
        {
            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.EmployeeLookups
                .Where(e => e.FirstName.ToLower().Contains(lowerSearchTerm) ||
                           e.LastName.ToLower().Contains(lowerSearchTerm) ||
                           (e.KnownName != null && e.KnownName.ToLower().Contains(lowerSearchTerm)) ||
                           e.PersonnelNumber.ToLower().Contains(lowerSearchTerm))
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<EmployeeLookup> CreateAsync(EmployeeLookup employeeLookup)
        {
            _context.EmployeeLookups.Add(employeeLookup);
            await _context.SaveChangesAsync();
            return employeeLookup;
        }

        public async Task<EmployeeLookup?> UpdateAsync(string personnelNumber, EmployeeLookup employeeLookup)
        {
            var existingEmployee = await _context.EmployeeLookups.FindAsync(personnelNumber);
            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.FirstName = employeeLookup.FirstName;
            existingEmployee.LastName = employeeLookup.LastName;
            existingEmployee.KnownName = employeeLookup.KnownName;
            existingEmployee.Initials = employeeLookup.Initials;
            existingEmployee.Race = employeeLookup.Race;
            existingEmployee.Gender = employeeLookup.Gender;
            existingEmployee.Disability = employeeLookup.Disability;
            existingEmployee.EELevel = employeeLookup.EELevel;
            existingEmployee.EECategory = employeeLookup.EECategory;

            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<bool> DeleteAsync(string personnelNumber)
        {
            var employee = await _context.EmployeeLookups.FindAsync(personnelNumber);
            if (employee == null)
            {
                return false;
            }

            _context.EmployeeLookups.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string personnelNumber)
        {
            return await _context.EmployeeLookups.AnyAsync(e => e.PersonnelNumber == personnelNumber);
        }

        public async Task<IEnumerable<EmployeeLookup>> GetByRaceAsync(string race)
        {
            return await _context.EmployeeLookups
                .Where(e => e.Race == race)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeLookup>> GetByGenderAsync(string gender)
        {
            return await _context.EmployeeLookups
                .Where(e => e.Gender == gender)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeLookup>> GetByEELevelAsync(string eeLevel)
        {
            return await _context.EmployeeLookups
                .Where(e => e.EELevel == eeLevel)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeLookup>> GetByEECategoryAsync(string eeCategory)
        {
            return await _context.EmployeeLookups
                .Where(e => e.EECategory == eeCategory)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeLookup>> GetWithDisabilityAsync(bool hasDisability)
        {
            return await _context.EmployeeLookups
                .Where(e => e.Disability == hasDisability)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }
    }
}