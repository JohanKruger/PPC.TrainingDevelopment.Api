using System;

namespace PPC.TrainingDevelopment.Api.Models
{
    public class TrainingPsMaster
    {
        public string? PersonnelNumber { get; set; }
        public string? Title { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Initials { get; set; }
        public string? SecondName { get; set; }
        public string? KnownAs { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IdNumber { get; set; }
        public string? RaceCode { get; set; }
        public string? RaceDescription { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyName { get; set; }
        public string? PersonnelArea { get; set; }
        public string? PersonnelAreaDescription { get; set; }
        public string? PersonnelSubArea { get; set; }
        public string? PersonnelSubAreaDescription { get; set; }
        public string? EmployeeGroup { get; set; }
        public string? EmployeeGroupDescription { get; set; }
        public string? EmployeeSubGroup { get; set; }
        public string? EmployeeSubGroupDescription { get; set; }
        public string? OrganisationUnit { get; set; }
        public string? OrganisationUnitDescription { get; set; }
        public string? Position { get; set; }
        public string? PositionDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CostCenter { get; set; }
        public string? CostCenterDescription { get; set; }
        public string? EmploymentStatus { get; set; }
        public string? EmployementStatusDescription { get; set; }
        public string? EmailAddress { get; set; }
        public string? Disability { get; set; }
        public string? EELevel { get; set; }
        public string? EECategory { get; set; }
        public string? JobGrade { get; set; }
        public string? ManagerPersonnelNumber { get; set; }
        public string? ManagerName { get; set; }
        public string? ManagerEmailAddress { get; set; }
        public string? ManagerKnownAs { get; set; }
        public string? ManagerCostCenter { get; set; }
    }
}