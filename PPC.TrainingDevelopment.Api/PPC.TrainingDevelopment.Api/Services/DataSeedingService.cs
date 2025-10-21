using PPC.TrainingDevelopment.Api.Data;
using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Services
{
    public class DataSeedingService
    {
        private readonly TrainingDevelopmentDbContext _context;

        public DataSeedingService(TrainingDevelopmentDbContext context)
        {
            _context = context;
        }

        public async Task SeedLookupValuesAsync()
        {
            await SeedLookupValuesDataAsync();
            await SeedEmployeeLookupDataAsync();
        }

        private async Task SeedLookupValuesDataAsync()
        {
            if (_context.LookupValues.Any())
            {
                return; // Data already seeded
            }

            var lookupValues = new List<LookupValue>
            {
                // Employee Status
                new LookupValue { LookupType = "EmployeeStatus", Value = "Active", Code = "ACTIVE", SortOrder = 1, IsActive = true },
                new LookupValue { LookupType = "EmployeeStatus", Value = "Inactive", Code = "INACTIVE", SortOrder = 2, IsActive = true },
                new LookupValue { LookupType = "EmployeeStatus", Value = "Terminated", Code = "TERM", SortOrder = 3, IsActive = true },

                // Gender
                new LookupValue { LookupType = "Gender", Value = "Male", Code = "M", SortOrder = 1, IsActive = true },
                new LookupValue { LookupType = "Gender", Value = "Female", Code = "F", SortOrder = 2, IsActive = true },
                new LookupValue { LookupType = "Gender", Value = "Other", Code = "O", SortOrder = 3, IsActive = true },

                // Race
                new LookupValue { LookupType = "Race", Value = "African", Code = "AFR", SortOrder = 1, IsActive = true },
                new LookupValue { LookupType = "Race", Value = "Coloured", Code = "COL", SortOrder = 2, IsActive = true },
                new LookupValue { LookupType = "Race", Value = "Indian", Code = "IND", SortOrder = 3, IsActive = true },
                new LookupValue { LookupType = "Race", Value = "White", Code = "WHT", SortOrder = 4, IsActive = true },

                // Department
                new LookupValue { LookupType = "Department", Value = "Information Technology", Code = "IT", SortOrder = 1, IsActive = true },
                new LookupValue { LookupType = "Department", Value = "Human Resources", Code = "HR", SortOrder = 2, IsActive = true },
                new LookupValue { LookupType = "Department", Value = "Finance", Code = "FIN", SortOrder = 3, IsActive = true },
                new LookupValue { LookupType = "Department", Value = "Operations", Code = "OPS", SortOrder = 4, IsActive = true },

                // EE Level
                new LookupValue { LookupType = "EELevel", Value = "Top Management", Code = "TM", SortOrder = 1, IsActive = true },
                new LookupValue { LookupType = "EELevel", Value = "Senior Management", Code = "SM", SortOrder = 2, IsActive = true },
                new LookupValue { LookupType = "EELevel", Value = "Professionally Qualified", Code = "PQ", SortOrder = 3, IsActive = true },
                new LookupValue { LookupType = "EELevel", Value = "Skilled Technical", Code = "ST", SortOrder = 4, IsActive = true },
                new LookupValue { LookupType = "EELevel", Value = "Semi-skilled", Code = "SS", SortOrder = 5, IsActive = true },
                new LookupValue { LookupType = "EELevel", Value = "Unskilled", Code = "US", SortOrder = 6, IsActive = true },

                // Training Status
                new LookupValue { LookupType = "TrainingStatus", Value = "Not Started", Code = "NS", SortOrder = 1, IsActive = true },
                new LookupValue { LookupType = "TrainingStatus", Value = "In Progress", Code = "IP", SortOrder = 2, IsActive = true },
                new LookupValue { LookupType = "TrainingStatus", Value = "Completed", Code = "COMP", SortOrder = 3, IsActive = true },
                new LookupValue { LookupType = "TrainingStatus", Value = "Cancelled", Code = "CANC", SortOrder = 4, IsActive = true },

                // Training Type
                new LookupValue { LookupType = "TrainingType", Value = "Skills Development", Code = "SD", SortOrder = 1, IsActive = true },
                new LookupValue { LookupType = "TrainingType", Value = "Learnership", Code = "LRN", SortOrder = 2, IsActive = true },
                new LookupValue { LookupType = "TrainingType", Value = "Internship", Code = "INT", SortOrder = 3, IsActive = true },
                new LookupValue { LookupType = "TrainingType", Value = "Bursary", Code = "BUR", SortOrder = 4, IsActive = true },
            };

            await _context.LookupValues.AddRangeAsync(lookupValues);
            await _context.SaveChangesAsync();
        }

        private async Task SeedEmployeeLookupDataAsync()
        {
            if (_context.EmployeeLookups.Any())
            {
                return; // Data already seeded
            }

            var employeeLookups = new List<EmployeeLookup>
            {
                // Sample Employee Records for Testing
                new EmployeeLookup { PersonnelNumber = "EMP001", FirstName = "John", LastName = "Smith", KnownName = "Johnny", Initials = "J.S.", Race = "African", Gender = "Male", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },
                new EmployeeLookup { PersonnelNumber = "EMP002", FirstName = "Sarah", LastName = "Johnson", KnownName = "Sarah", Initials = "S.J.", Race = "White", Gender = "Female", Disability = false, EELevel = "Senior Management", EECategory = "Management" },
                new EmployeeLookup { PersonnelNumber = "EMP003", FirstName = "Michael", LastName = "Brown", KnownName = "Mike", Initials = "M.B.", Race = "Coloured", Gender = "Male", Disability = true, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP004", FirstName = "Lisa", LastName = "Davis", KnownName = "Lisa", Initials = "L.D.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },
                new EmployeeLookup { PersonnelNumber = "EMP005", FirstName = "David", LastName = "Wilson", KnownName = "Dave", Initials = "D.W.", Race = "White", Gender = "Male", Disability = false, EELevel = "Top Management", EECategory = "Executive" },
                new EmployeeLookup { PersonnelNumber = "EMP006", FirstName = "Jennifer", LastName = "Miller", KnownName = "Jen", Initials = "J.M.", Race = "African", Gender = "Female", Disability = false, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP007", FirstName = "Robert", LastName = "Garcia", KnownName = "Bob", Initials = "R.G.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP008", FirstName = "Emily", LastName = "Rodriguez", KnownName = "Emily", Initials = "E.R.", Race = "Indian", Gender = "Female", Disability = true, EELevel = "Professionally Qualified", EECategory = "Professional" },
                new EmployeeLookup { PersonnelNumber = "EMP009", FirstName = "James", LastName = "Martinez", KnownName = "Jim", Initials = "J.M.", Race = "African", Gender = "Male", Disability = false, EELevel = "Senior Management", EECategory = "Management" },
                new EmployeeLookup { PersonnelNumber = "EMP010", FirstName = "Amanda", LastName = "Anderson", KnownName = "Amanda", Initials = "A.A.", Race = "White", Gender = "Female", Disability = false, EELevel = "Unskilled", EECategory = "General" },

                new EmployeeLookup { PersonnelNumber = "EMP011", FirstName = "Christopher", LastName = "Taylor", KnownName = "Chris", Initials = "C.T.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },
                new EmployeeLookup { PersonnelNumber = "EMP012", FirstName = "Jessica", LastName = "Thomas", KnownName = "Jess", Initials = "J.T.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP013", FirstName = "Matthew", LastName = "Jackson", KnownName = "Matt", Initials = "M.J.", Race = "African", Gender = "Male", Disability = true, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP014", FirstName = "Ashley", LastName = "White", KnownName = "Ash", Initials = "A.W.", Race = "White", Gender = "Female", Disability = false, EELevel = "Senior Management", EECategory = "Management" },
                new EmployeeLookup { PersonnelNumber = "EMP015", FirstName = "Joshua", LastName = "Harris", KnownName = "Josh", Initials = "J.H.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Top Management", EECategory = "Executive" },
                new EmployeeLookup { PersonnelNumber = "EMP016", FirstName = "Samantha", LastName = "Martin", KnownName = "Sam", Initials = "S.M.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },
                new EmployeeLookup { PersonnelNumber = "EMP017", FirstName = "Andrew", LastName = "Thompson", KnownName = "Andy", Initials = "A.T.", Race = "African", Gender = "Male", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP018", FirstName = "Stephanie", LastName = "Garcia", KnownName = "Steph", Initials = "S.G.", Race = "White", Gender = "Female", Disability = true, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP019", FirstName = "Brian", LastName = "Martinez", KnownName = "Brian", Initials = "B.M.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Unskilled", EECategory = "General" },
                new EmployeeLookup { PersonnelNumber = "EMP020", FirstName = "Nicole", LastName = "Robinson", KnownName = "Nikki", Initials = "N.R.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },

                new EmployeeLookup { PersonnelNumber = "EMP021", FirstName = "Kevin", LastName = "Clark", KnownName = "Kev", Initials = "K.C.", Race = "African", Gender = "Male", Disability = false, EELevel = "Senior Management", EECategory = "Management" },
                new EmployeeLookup { PersonnelNumber = "EMP022", FirstName = "Rachel", LastName = "Rodriguez", KnownName = "Rachel", Initials = "R.R.", Race = "White", Gender = "Female", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP023", FirstName = "Daniel", LastName = "Lewis", KnownName = "Dan", Initials = "D.L.", Race = "Coloured", Gender = "Male", Disability = true, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP024", FirstName = "Laura", LastName = "Lee", KnownName = "Laura", Initials = "L.L.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Top Management", EECategory = "Executive" },
                new EmployeeLookup { PersonnelNumber = "EMP025", FirstName = "Ryan", LastName = "Walker", KnownName = "Ryan", Initials = "R.W.", Race = "African", Gender = "Male", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },
                new EmployeeLookup { PersonnelNumber = "EMP026", FirstName = "Michelle", LastName = "Hall", KnownName = "Shell", Initials = "M.H.", Race = "White", Gender = "Female", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP027", FirstName = "Jason", LastName = "Allen", KnownName = "Jason", Initials = "J.A.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Unskilled", EECategory = "General" },
                new EmployeeLookup { PersonnelNumber = "EMP028", FirstName = "Kimberly", LastName = "Young", KnownName = "Kim", Initials = "K.Y.", Race = "Indian", Gender = "Female", Disability = true, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP029", FirstName = "Mark", LastName = "Hernandez", KnownName = "Mark", Initials = "M.H.", Race = "African", Gender = "Male", Disability = false, EELevel = "Senior Management", EECategory = "Management" },
                new EmployeeLookup { PersonnelNumber = "EMP030", FirstName = "Amy", LastName = "King", KnownName = "Amy", Initials = "A.K.", Race = "White", Gender = "Female", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },

                new EmployeeLookup { PersonnelNumber = "EMP031", FirstName = "Steven", LastName = "Wright", KnownName = "Steve", Initials = "S.W.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP032", FirstName = "Donna", LastName = "Lopez", KnownName = "Donna", Initials = "D.L.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Top Management", EECategory = "Executive" },
                new EmployeeLookup { PersonnelNumber = "EMP033", FirstName = "Kenneth", LastName = "Hill", KnownName = "Ken", Initials = "K.H.", Race = "African", Gender = "Male", Disability = true, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP034", FirstName = "Carol", LastName = "Scott", KnownName = "Carol", Initials = "C.S.", Race = "White", Gender = "Female", Disability = false, EELevel = "Unskilled", EECategory = "General" },
                new EmployeeLookup { PersonnelNumber = "EMP035", FirstName = "Paul", LastName = "Green", KnownName = "Paul", Initials = "P.G.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },
                new EmployeeLookup { PersonnelNumber = "EMP036", FirstName = "Sharon", LastName = "Adams", KnownName = "Sharon", Initials = "S.A.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Senior Management", EECategory = "Management" },
                new EmployeeLookup { PersonnelNumber = "EMP037", FirstName = "Eric", LastName = "Baker", KnownName = "Eric", Initials = "E.B.", Race = "African", Gender = "Male", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP038", FirstName = "Cynthia", LastName = "Gonzalez", KnownName = "Cindy", Initials = "C.G.", Race = "White", Gender = "Female", Disability = true, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP039", FirstName = "Stephen", LastName = "Nelson", KnownName = "Steve", Initials = "S.N.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Top Management", EECategory = "Executive" },
                new EmployeeLookup { PersonnelNumber = "EMP040", FirstName = "Helen", LastName = "Carter", KnownName = "Helen", Initials = "H.C.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },

                new EmployeeLookup { PersonnelNumber = "EMP041", FirstName = "Raymond", LastName = "Mitchell", KnownName = "Ray", Initials = "R.M.", Race = "African", Gender = "Male", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP042", FirstName = "Sandra", LastName = "Perez", KnownName = "Sandy", Initials = "S.P.", Race = "White", Gender = "Female", Disability = false, EELevel = "Unskilled", EECategory = "General" },
                new EmployeeLookup { PersonnelNumber = "EMP043", FirstName = "Gregory", LastName = "Roberts", KnownName = "Greg", Initials = "G.R.", Race = "Coloured", Gender = "Male", Disability = true, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP044", FirstName = "Betty", LastName = "Turner", KnownName = "Betty", Initials = "B.T.", Race = "Indian", Gender = "Female", Disability = false, EELevel = "Senior Management", EECategory = "Management" },
                new EmployeeLookup { PersonnelNumber = "EMP045", FirstName = "Edward", LastName = "Phillips", KnownName = "Ed", Initials = "E.P.", Race = "African", Gender = "Male", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" },
                new EmployeeLookup { PersonnelNumber = "EMP046", FirstName = "Dorothy", LastName = "Campbell", KnownName = "Dot", Initials = "D.C.", Race = "White", Gender = "Female", Disability = false, EELevel = "Top Management", EECategory = "Executive" },
                new EmployeeLookup { PersonnelNumber = "EMP047", FirstName = "Jerry", LastName = "Parker", KnownName = "Jerry", Initials = "J.P.", Race = "Coloured", Gender = "Male", Disability = false, EELevel = "Skilled Technical", EECategory = "Technical" },
                new EmployeeLookup { PersonnelNumber = "EMP048", FirstName = "Lisa", LastName = "Evans", KnownName = "Lisa", Initials = "L.E.", Race = "Indian", Gender = "Female", Disability = true, EELevel = "Semi-skilled", EECategory = "Operator" },
                new EmployeeLookup { PersonnelNumber = "EMP049", FirstName = "Frank", LastName = "Edwards", KnownName = "Frank", Initials = "F.E.", Race = "African", Gender = "Male", Disability = false, EELevel = "Unskilled", EECategory = "General" },
                new EmployeeLookup { PersonnelNumber = "EMP050", FirstName = "Maria", LastName = "Collins", KnownName = "Maria", Initials = "M.C.", Race = "White", Gender = "Female", Disability = false, EELevel = "Professionally Qualified", EECategory = "Professional" }
            };

            await _context.EmployeeLookups.AddRangeAsync(employeeLookups);
            await _context.SaveChangesAsync();
        }
    }
}