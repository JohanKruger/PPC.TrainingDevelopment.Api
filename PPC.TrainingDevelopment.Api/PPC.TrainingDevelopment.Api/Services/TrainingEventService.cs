using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class TrainingEventService : ITrainingEventService
    {
        private readonly TrainingDevelopmentDbContext _context;

        public TrainingEventService(TrainingDevelopmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingEvent>> GetAllAsync()
        {
            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .OrderBy(te => te.TrainingEventName.Value)
                .ThenBy(te => te.EventType.Value)
                .ToListAsync();
        }

        public async Task<TrainingEvent?> GetByIdAsync(int trainingEventId)
        {
            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .FirstOrDefaultAsync(te => te.TrainingEventId == trainingEventId);
        }

        public async Task<IEnumerable<TrainingEvent>> GetByPersonnelNumberAsync(string personnelNumber)
        {
            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .Where(te => te.PersonnelNumber == personnelNumber)
                .OrderBy(te => te.TrainingEventName!.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingEvent>> GetByIDNumberAsync(string idNumber)
        {
            return await _context.TrainingEvents
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .Where(te => te.IDNumber == idNumber)
                .OrderBy(te => te.TrainingEventName!.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingEvent>> GetByEventTypeAsync(int eventTypeId)
        {
            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .Where(te => te.EventTypeId == eventTypeId)
                .OrderBy(te => te.TrainingEventName!.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingEvent>> GetByRegionAsync(int regionId)
        {
            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .Where(te => te.RegionId == regionId)
                .OrderBy(te => te.TrainingEventName!.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingEvent>> GetByProvinceAsync(int provinceId)
        {
            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .Where(te => te.ProvinceId == provinceId)
                .OrderBy(te => te.TrainingEventName!.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingEvent>> GetByMunicipalityAsync(int municipalityId)
        {
            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .Where(te => te.MunicipalityId == municipalityId)
                .OrderBy(te => te.TrainingEventName!.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingEvent>> GetBySiteAsync(int siteId)
        {
            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .Where(te => te.SiteId == siteId)
                .OrderBy(te => te.TrainingEventName!.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingEvent>> SearchByTrainingEventNameAsync(string searchTerm)
        {
            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.TrainingEvents
                .Include(te => te.Employee)
                .Include(te => te.NonEmployee)
                .Include(te => te.EventType)
                .Include(te => te.TrainingEventName)
                .Include(te => te.Region)
                .Include(te => te.Province)
                .Include(te => te.Municipality)
                .Include(te => te.Site)
                .Where(te => te.TrainingEventName!.Value.ToLower().Contains(lowerSearchTerm) ||
                           te.EventType!.Value.ToLower().Contains(lowerSearchTerm))
                .OrderBy(te => te.TrainingEventName!.Value)
                .ToListAsync();
        }

        public async Task<TrainingEvent> CreateAsync(TrainingEvent trainingEvent)
        {
            // Validate that either PersonnelNumber or IDNumber is provided, but not both
            if (!await ValidateParticipantAsync(trainingEvent.PersonnelNumber, trainingEvent.IDNumber))
            {
                throw new ArgumentException("Either PersonnelNumber or IDNumber must be provided, but not both.");
            }

            _context.TrainingEvents.Add(trainingEvent);
            await _context.SaveChangesAsync();
            return trainingEvent;
        }

        public async Task<TrainingEvent?> UpdateAsync(int trainingEventId, TrainingEvent trainingEvent)
        {
            var existingEvent = await _context.TrainingEvents.FindAsync(trainingEventId);
            if (existingEvent == null)
            {
                return null;
            }

            // Validate that either PersonnelNumber or IDNumber is provided, but not both
            if (!await ValidateParticipantAsync(trainingEvent.PersonnelNumber, trainingEvent.IDNumber))
            {
                throw new ArgumentException("Either PersonnelNumber or IDNumber must be provided, but not both.");
            }

            existingEvent.PersonnelNumber = trainingEvent.PersonnelNumber;
            existingEvent.IDNumber = trainingEvent.IDNumber;
            existingEvent.EventTypeId = trainingEvent.EventTypeId;
            existingEvent.TrainingEventNameId = trainingEvent.TrainingEventNameId;
            existingEvent.RegionId = trainingEvent.RegionId;
            existingEvent.ProvinceId = trainingEvent.ProvinceId;
            existingEvent.MunicipalityId = trainingEvent.MunicipalityId;
            existingEvent.SiteId = trainingEvent.SiteId;

            await _context.SaveChangesAsync();
            return existingEvent;
        }

        public async Task<bool> DeleteAsync(int trainingEventId)
        {
            var trainingEvent = await _context.TrainingEvents.FindAsync(trainingEventId);
            if (trainingEvent == null)
            {
                return false;
            }

            _context.TrainingEvents.Remove(trainingEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int trainingEventId)
        {
            return await _context.TrainingEvents.AnyAsync(te => te.TrainingEventId == trainingEventId);
        }

        public Task<bool> ValidateParticipantAsync(string? personnelNumber, string? idNumber)
        {
            // Either PersonnelNumber or IDNumber must be provided, but not both
            var hasPersonnelNumber = !string.IsNullOrWhiteSpace(personnelNumber);
            var hasIDNumber = !string.IsNullOrWhiteSpace(idNumber);

            return Task.FromResult(hasPersonnelNumber != hasIDNumber); // XOR - exactly one must be true
        }
    }
}