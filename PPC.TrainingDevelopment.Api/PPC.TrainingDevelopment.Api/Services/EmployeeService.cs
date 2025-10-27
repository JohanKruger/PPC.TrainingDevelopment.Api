using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly TrainingDevelopmentDbContext _context;

        public EmployeeService(TrainingDevelopmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<Employee?> GetByPersonnelNumberAsync(string personnelNumber)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.PersonnelNumber == personnelNumber || e.IDNumber == personnelNumber);
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string searchTerm)
        {
            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Employees
                .Where(e => e.FirstName.ToLower().Contains(lowerSearchTerm) ||
                           e.LastName.ToLower().Contains(lowerSearchTerm) ||
                           (e.KnownName != null && e.KnownName.ToLower().Contains(lowerSearchTerm)) ||
                           e.PersonnelNumber.ToLower().Contains(lowerSearchTerm) ||
                           (e.JobTitle != null && e.JobTitle.ToLower().Contains(lowerSearchTerm)) ||
                           (e.JobGrade != null && e.JobGrade.ToLower().Contains(lowerSearchTerm)) ||
                           (e.IDNumber != null && e.IDNumber.ToLower().Contains(lowerSearchTerm)) ||
                           (e.Site != null && e.Site.ToLower().Contains(lowerSearchTerm)) ||
                           (e.Notes != null && e.Notes.ToLower().Contains(lowerSearchTerm)))
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateAsync(string personnelNumber, Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(personnelNumber);
            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.KnownName = employee.KnownName;
            existingEmployee.Initials = employee.Initials;
            existingEmployee.Race = employee.Race;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.Disability = employee.Disability;
            existingEmployee.EELevel = employee.EELevel;
            existingEmployee.EECategory = employee.EECategory;
            existingEmployee.JobTitle = employee.JobTitle;
            existingEmployee.JobGrade = employee.JobGrade;
            existingEmployee.IDNumber = employee.IDNumber;
            existingEmployee.Site = employee.Site;
            existingEmployee.HighestQualification = employee.HighestQualification;
            existingEmployee.Notes = employee.Notes;

            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<bool> DeleteAsync(string personnelNumber)
        {
            var employee = await _context.Employees.FindAsync(personnelNumber);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string personnelNumber)
        {
            return await _context.Employees.AnyAsync(e => e.PersonnelNumber == personnelNumber);
        }

        public async Task<IEnumerable<Employee>> GetByRaceAsync(string race)
        {
            return await _context.Employees
                .Where(e => e.Race == race)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByGenderAsync(string gender)
        {
            return await _context.Employees
                .Where(e => e.Gender == gender)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByEELevelAsync(string eeLevel)
        {
            return await _context.Employees
                .Where(e => e.EELevel == eeLevel)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByEECategoryAsync(string eeCategory)
        {
            return await _context.Employees
                .Where(e => e.EECategory == eeCategory)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetWithDisabilityAsync(bool hasDisability)
        {
            return await _context.Employees
                .Where(e => e.Disability == hasDisability)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByJobTitleAsync(string jobTitle)
        {
            return await _context.Employees
                .Where(e => e.JobTitle == jobTitle)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByJobGradeAsync(string jobGrade)
        {
            return await _context.Employees
                .Where(e => e.JobGrade == jobGrade)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetBySiteAsync(string site)
        {
            return await _context.Employees
                .Where(e => e.Site == site)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIDNumberAsync(string idNumber)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.IDNumber == idNumber);
        }
    }
}