using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class TrainingRecordEventService : ITrainingRecordEventService
    {
        private readonly TrainingDevelopmentDbContext _context;

        public TrainingRecordEventService(TrainingDevelopmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingRecordEvent>> GetAllAsync()
        {
            return await _context.TrainingRecordEvents
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Employee)
                .OrderBy(tre => tre.StartDate)
                .ThenBy(tre => tre.TrainingEventId)
                .ToListAsync();
        }

        public async Task<TrainingRecordEvent?> GetByIdAsync(int trainingRecordEventId)
        {
            return await _context.TrainingRecordEvents
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Employee)
                .FirstOrDefaultAsync(tre => tre.TrainingRecordEventId == trainingRecordEventId);
        }

        public async Task<IEnumerable<TrainingRecordEvent>> GetByTrainingEventIdAsync(int trainingEventId)
        {
            return await _context.TrainingRecordEvents
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Employee)
                .Where(tre => tre.TrainingEventId == trainingEventId)
                .OrderBy(tre => tre.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingRecordEvent>> GetByPersonnelNumberAsync(string personnelNumber)
        {
            return await _context.TrainingRecordEvents
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Employee)
                .Where(tre => tre.PersonnelNumber == personnelNumber)
                .OrderBy(tre => tre.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingRecordEvent>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TrainingRecordEvents
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Employee)
                .Where(tre => tre.StartDate >= startDate && tre.EndDate <= endDate)
                .OrderBy(tre => tre.StartDate)
                .ThenBy(tre => tre.TrainingEventId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingRecordEvent>> GetWithEvidenceAsync()
        {
            return await _context.TrainingRecordEvents
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Employee)
                .Where(tre => tre.Evidence == true)
                .OrderBy(tre => tre.StartDate)
                .ThenBy(tre => tre.TrainingEventId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingRecordEvent>> GetWithoutEvidenceAsync()
        {
            return await _context.TrainingRecordEvents
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Employee)
                .Where(tre => tre.Evidence == false || tre.Evidence == null)
                .OrderBy(tre => tre.StartDate)
                .ThenBy(tre => tre.TrainingEventId)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalCostsByTrainingEventIdAsync(int trainingEventId)
        {
            var costs = await _context.TrainingRecordEvents
                .Where(tre => tre.TrainingEventId == trainingEventId)
                .Select(tre => new
                {
                    CostTrainingMaterials = tre.CostTrainingMaterials ?? 0,
                    CostTrainers = tre.CostTrainers ?? 0,
                    CostTrainingFacilities = tre.CostTrainingFacilities ?? 0,
                    ScholarshipsBursaries = tre.ScholarshipsBursaries ?? 0,
                    CourseFees = tre.CourseFees ?? 0,
                    Accommodation = tre.Accommodation ?? 0,
                    Travel = tre.Travel ?? 0,
                    Meal = tre.Meal ?? 0,
                    AdministrationCosts = tre.AdministrationCosts ?? 0,
                    EquipmentDepreciation = tre.EquipmentDepreciation ?? 0
                })
                .ToListAsync();

            return costs.Sum(c => c.CostTrainingMaterials + c.CostTrainers + c.CostTrainingFacilities +
                                 c.ScholarshipsBursaries + c.CourseFees + c.Accommodation + c.Travel +
                                 c.Meal + c.AdministrationCosts + c.EquipmentDepreciation);
        }

        public async Task<decimal> GetTotalCostsByPersonnelNumberAsync(string personnelNumber)
        {
            var costs = await _context.TrainingRecordEvents
                .Where(tre => tre.PersonnelNumber == personnelNumber)
                .Select(tre => new
                {
                    CostTrainingMaterials = tre.CostTrainingMaterials ?? 0,
                    CostTrainers = tre.CostTrainers ?? 0,
                    CostTrainingFacilities = tre.CostTrainingFacilities ?? 0,
                    ScholarshipsBursaries = tre.ScholarshipsBursaries ?? 0,
                    CourseFees = tre.CourseFees ?? 0,
                    Accommodation = tre.Accommodation ?? 0,
                    Travel = tre.Travel ?? 0,
                    Meal = tre.Meal ?? 0,
                    AdministrationCosts = tre.AdministrationCosts ?? 0,
                    EquipmentDepreciation = tre.EquipmentDepreciation ?? 0
                })
                .ToListAsync();

            return costs.Sum(c => c.CostTrainingMaterials + c.CostTrainers + c.CostTrainingFacilities +
                                 c.ScholarshipsBursaries + c.CourseFees + c.Accommodation + c.Travel +
                                 c.Meal + c.AdministrationCosts + c.EquipmentDepreciation);
        }

        public async Task<decimal> GetTotalCostsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var costs = await _context.TrainingRecordEvents
                .Where(tre => tre.StartDate >= startDate && tre.EndDate <= endDate)
                .Select(tre => new
                {
                    CostTrainingMaterials = tre.CostTrainingMaterials ?? 0,
                    CostTrainers = tre.CostTrainers ?? 0,
                    CostTrainingFacilities = tre.CostTrainingFacilities ?? 0,
                    ScholarshipsBursaries = tre.ScholarshipsBursaries ?? 0,
                    CourseFees = tre.CourseFees ?? 0,
                    Accommodation = tre.Accommodation ?? 0,
                    Travel = tre.Travel ?? 0,
                    Meal = tre.Meal ?? 0,
                    AdministrationCosts = tre.AdministrationCosts ?? 0,
                    EquipmentDepreciation = tre.EquipmentDepreciation ?? 0
                })
                .ToListAsync();

            return costs.Sum(c => c.CostTrainingMaterials + c.CostTrainers + c.CostTrainingFacilities +
                                 c.ScholarshipsBursaries + c.CourseFees + c.Accommodation + c.Travel +
                                 c.Meal + c.AdministrationCosts + c.EquipmentDepreciation);
        }

        public async Task<TrainingRecordEvent> CreateAsync(TrainingRecordEvent trainingRecordEvent)
        {
            // Validate date range
            if (!await ValidateDateRangeAsync(trainingRecordEvent.StartDate, trainingRecordEvent.EndDate))
            {
                throw new ArgumentException("End date must be greater than or equal to start date.");
            }

            // Validate that the training event exists
            if (!await ValidateTrainingEventExistsAsync(trainingRecordEvent.TrainingEventId))
            {
                throw new ArgumentException($"Training event with ID {trainingRecordEvent.TrainingEventId} does not exist.");
            }

            _context.TrainingRecordEvents.Add(trainingRecordEvent);
            await _context.SaveChangesAsync();
            return trainingRecordEvent;
        }

        public async Task<TrainingRecordEvent?> UpdateAsync(int trainingRecordEventId, TrainingRecordEvent trainingRecordEvent)
        {
            var existingRecord = await _context.TrainingRecordEvents.FindAsync(trainingRecordEventId);
            if (existingRecord == null)
            {
                return null;
            }

            // Validate date range
            if (!await ValidateDateRangeAsync(trainingRecordEvent.StartDate, trainingRecordEvent.EndDate))
            {
                throw new ArgumentException("End date must be greater than or equal to start date.");
            }

            // Validate that the training event exists
            if (!await ValidateTrainingEventExistsAsync(trainingRecordEvent.TrainingEventId))
            {
                throw new ArgumentException($"Training event with ID {trainingRecordEvent.TrainingEventId} does not exist.");
            }

            existingRecord.TrainingEventId = trainingRecordEvent.TrainingEventId;
            existingRecord.StartDate = trainingRecordEvent.StartDate;
            existingRecord.EndDate = trainingRecordEvent.EndDate;
            existingRecord.Hours = trainingRecordEvent.Hours;
            existingRecord.Minutes = trainingRecordEvent.Minutes;
            existingRecord.PersonnelNumber = trainingRecordEvent.PersonnelNumber;
            existingRecord.Evidence = trainingRecordEvent.Evidence;
            existingRecord.ServiceProviderExternal = trainingRecordEvent.ServiceProviderExternal;
            existingRecord.CostTrainingMaterials = trainingRecordEvent.CostTrainingMaterials;
            existingRecord.CostTrainers = trainingRecordEvent.CostTrainers;
            existingRecord.CostTrainingFacilities = trainingRecordEvent.CostTrainingFacilities;
            existingRecord.ScholarshipsBursaries = trainingRecordEvent.ScholarshipsBursaries;
            existingRecord.CourseFees = trainingRecordEvent.CourseFees;
            existingRecord.Accommodation = trainingRecordEvent.Accommodation;
            existingRecord.Travel = trainingRecordEvent.Travel;
            existingRecord.Meal = trainingRecordEvent.Meal;
            existingRecord.AdministrationCosts = trainingRecordEvent.AdministrationCosts;
            existingRecord.EquipmentDepreciation = trainingRecordEvent.EquipmentDepreciation;

            await _context.SaveChangesAsync();
            return existingRecord;
        }

        public async Task<bool> DeleteAsync(int trainingRecordEventId)
        {
            var trainingRecordEvent = await _context.TrainingRecordEvents.FindAsync(trainingRecordEventId);
            if (trainingRecordEvent == null)
            {
                return false;
            }

            _context.TrainingRecordEvents.Remove(trainingRecordEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int trainingRecordEventId)
        {
            return await _context.TrainingRecordEvents.AnyAsync(tre => tre.TrainingRecordEventId == trainingRecordEventId);
        }

        public Task<bool> ValidateDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return Task.FromResult(endDate >= startDate);
        }

        public async Task<bool> ValidateTrainingEventExistsAsync(int trainingEventId)
        {
            return await _context.TrainingEvents.AnyAsync(te => te.TrainingEventId == trainingEventId);
        }
    }
}