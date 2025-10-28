using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models.Response;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class ReportsService : IReportsService
    {
        private readonly TrainingDevelopmentDbContext _context;

        public ReportsService(TrainingDevelopmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync()
        {
            return await BuildTrainingReportQuery().ToListAsync();
        }

        public async Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(DateTime startDate, DateTime endDate)
        {
            return await BuildTrainingReportQuery()
                .Where(x => x.StartDate >= startDate && x.EndDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(string personnelNumber)
        {
            return await BuildTrainingReportQuery()
                .Where(x => x.PersonnelNumber == personnelNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsByEventForExportAsync(int trainingEventId)
        {
            return await BuildTrainingReportQuery()
                .Where(x => x.TrainingEventId == trainingEventId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingReportResponse>> GetTrainingRecordsForExportAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? personnelNumber = null,
            int? trainingEventId = null,
            bool? hasEvidence = null)
        {
            var query = BuildTrainingReportQuery();

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            if (!string.IsNullOrWhiteSpace(personnelNumber))
                query = query.Where(x => x.PersonnelNumber == personnelNumber);

            if (trainingEventId.HasValue)
                query = query.Where(x => x.TrainingEventId == trainingEventId.Value);

            if (hasEvidence.HasValue)
                query = query.Where(x => x.Evidence == hasEvidence.Value);

            return await query.ToListAsync();
        }

        private IQueryable<TrainingReportResponse> BuildTrainingReportQuery()
        {
            return _context.TrainingRecordEvents
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Employee)
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.EventType)
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.TrainingEventName)
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Region)
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Province)
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Municipality)
                .Include(tre => tre.TrainingEvent)
                    .ThenInclude(te => te!.Site)
                .Select(tre => new TrainingReportResponse
                {
                    // Training Record Event fields
                    TrainingRecordEventId = tre.TrainingRecordEventId,
                    StartDate = tre.StartDate,
                    EndDate = tre.EndDate,
                    Hours = tre.Hours,
                    Minutes = tre.Minutes,
                    Evidence = tre.Evidence,
                    ServiceProviderExternal = tre.ServiceProviderExternal,
                    CostTrainingMaterials = tre.CostTrainingMaterials,
                    CostTrainers = tre.CostTrainers,
                    CostTrainingFacilities = tre.CostTrainingFacilities,
                    ScholarshipsBursaries = tre.ScholarshipsBursaries,
                    CourseFees = tre.CourseFees,
                    Accommodation = tre.Accommodation,
                    Travel = tre.Travel,
                    Meal = tre.Meal,
                    AdministrationCosts = tre.AdministrationCosts,
                    EquipmentDepreciation = tre.EquipmentDepreciation,
                    TotalCosts = tre.TotalCosts,
                    TotalDurationMinutes = tre.TotalDurationMinutes,

                    // Training Event fields
                    TrainingEventId = tre.TrainingEventId,
                    EventType = tre.TrainingEvent!.EventType != null ? tre.TrainingEvent.EventType.Value : null,
                    TrainingEventName = tre.TrainingEvent!.TrainingEventName != null ? tre.TrainingEvent.TrainingEventName.Value : null,
                    Region = tre.TrainingEvent!.Region != null ? tre.TrainingEvent.Region.Value : null,
                    Province = tre.TrainingEvent!.Province != null ? tre.TrainingEvent.Province.Value : null,
                    Municipality = tre.TrainingEvent!.Municipality != null ? tre.TrainingEvent.Municipality.Value : null,
                    Site = tre.TrainingEvent!.Site != null ? tre.TrainingEvent.Site.Value : null,

                    // Employee fields (when linked via PersonnelNumber)
                    PersonnelNumber = tre.PersonnelNumber ?? tre.TrainingEvent.PersonnelNumber,
                    EmployeeFirstName = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.FirstName : null,
                    EmployeeLastName = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.LastName : null,
                    EmployeeKnownName = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.KnownName : null,
                    EmployeeInitials = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.Initials : null,
                    EmployeeRace = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.Race : null,
                    EmployeeGender = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.Gender : null,
                    EmployeeDisability = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.Disability : null,
                    EmployeeEELevel = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.EELevel : null,
                    EmployeeEECategory = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.EECategory : null,
                    EmployeeJobTitle = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.JobTitle : null,
                    EmployeeJobGrade = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.JobGrade : null,
                    EmployeeIDNumber = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.IDNumber : null,
                    EmployeeSite = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.Site : null,
                    EmployeeHighestQualification = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.HighestQualification : null,
                    EmployeeNotes = tre.TrainingEvent.Employee != null ? tre.TrainingEvent.Employee.Notes : null,

                    // Non-Employee ID Number (when no employee match exists)
                    NonEmployeeIDNumber = tre.TrainingEvent.Employee == null ? tre.TrainingEvent.IDNumber : null
                })
                .OrderBy(x => x.StartDate)
                .ThenBy(x => x.TrainingRecordEventId);
        }
    }
}