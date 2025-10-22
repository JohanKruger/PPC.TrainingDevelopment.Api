using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class TrainingPsMasterService
    {
        private readonly string _connectionString =
            "User Id=HR_TR_USER;Password=xXfhv7tWhISac6ay;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.2.251.17)(PORT=1527)))(CONNECT_DATA=(SID=PRD)));";

        public async Task<List<TrainingPsMaster>> GetAllAsync()
        {
            var result = new List<TrainingPsMaster>();
            using var conn = new OracleConnection(_connectionString);
            using var cmd = new OracleCommand("SELECT * FROM SAPBIUSER.MV_EMP_TRAINING_PS_MASTER WHERE ROWNUM <= 50 AND END_DATE > TRUNC(SYSDATE)         ", conn);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(Map(reader));
            }
            return result;
        }

        public async Task<TrainingPsMaster?> GetByPersonnelNumberAsync(string personnelNumber)
        {
            using var conn = new OracleConnection(_connectionString);
            using var cmd = new OracleCommand("SELECT * FROM SAPBIUSER.MV_EMP_TRAINING_PS_MASTER WHERE (PERSONNEL_NUMBER = :PersonnelNumber OR ID_NUMBER = :PersonnelNumber) AND END_DATE > TRUNC(SYSDATE) AND ROWNUM <= 50", conn);
            cmd.Parameters.Add(":PersonnelNumber", OracleDbType.NVarchar2).Value = personnelNumber;
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return Map(reader);
            }
            return null;
        }

        private TrainingPsMaster Map(IDataRecord r)
        {
            return new TrainingPsMaster
            {
                PersonnelNumber = r["PERSONNEL_NUMBER"] as string,
                Title = r["TITLE"] as string,
                LastName = r["LAST_NAME"] as string,
                FirstName = r["FIRST_NAME"] as string,
                Initials = r["INITIALS"] as string,
                SecondName = r["SECOND_NAME"] as string,
                KnownAs = r["KNOWN_AS"] as string,
                Gender = r["GENDER"] as string,
                DateOfBirth = r["DATE_OF_BIRTH"] == DBNull.Value ? null : (DateTime?)r["DATE_OF_BIRTH"],
                IdNumber = r["ID_NUMBER"] as string,
                RaceCode = r["RACE_CODE"] as string,
                RaceDescription = r["RACE_DESCRIPTION"] as string,
                CompanyCode = r["COMPANY_CODE"] as string,
                CompanyName = r["COMPANY_NAME"] as string,
                PersonnelArea = r["PERSONNEL_AREA"] as string,
                PersonnelAreaDescription = r["PERSONNEL_AREA_DESCRIPTION"] as string,
                PersonnelSubArea = r["PERSONNEL_SUB_AREA"] as string,
                PersonnelSubAreaDescription = r["PERSONNEL_SUB_AREA_DESCRIPTION"] as string,
                EmployeeGroup = r["EMPLOYEE_GROUP"] as string,
                EmployeeGroupDescription = r["EMPLOYEE_GROUP_DESCRIPTION"] as string,
                EmployeeSubGroup = r["EMPLOYEE_SUB_GROUP"] as string,
                EmployeeSubGroupDescription = r["EMPLOYEE_SUB_GROUP_DESCRIPTION"] as string,
                OrganisationUnit = r["ORGANISATION_UNIT"] as string,
                OrganisationUnitDescription = r["ORGANISATION_UNIT_DESCRIPTION"] as string,
                Position = r["POSITION"] as string,
                PositionDescription = r["POSITION_DESCRIPTION"] as string,
                StartDate = r["START_DATE"] == DBNull.Value ? null : (DateTime?)r["START_DATE"],
                EndDate = r["END_DATE"] == DBNull.Value ? null : (DateTime?)r["END_DATE"],
                CostCenter = r["COST_CENTER"] as string,
                CostCenterDescription = r["COST_CENTER_DESCRIPTION"] as string,
                EmploymentStatus = r["EMPLOYMENT_STATUS"] as string,
                EmployementStatusDescription = r["EMPLOYEMENT_STATUS_DESCRIPTION"] as string,
                EmailAddress = r["EMAIL_ADDRESS"] as string,
                Disability = r["DISABILITY"] as string,
                EELevel = r["EE_LEVEL"] as string,
                EECategory = r["EE_CATEGORY"] as string,
                JobGrade = r["JOB_GRADE"] as string,
                ManagerPersonnelNumber = r["MANAGER_PERSONNEL_NUMBER"] as string,
                ManagerName = r["MANAGER_NAME"] as string,
                ManagerEmailAddress = r["MANAGER_EMAIL_ADDRESS"] as string,
                ManagerKnownAs = r["MANAGER_KNOWN_AS"] as string,
                ManagerCostCenter = r["MANAGER_COST_CENTER"] as string
            };
        }
    }
}
