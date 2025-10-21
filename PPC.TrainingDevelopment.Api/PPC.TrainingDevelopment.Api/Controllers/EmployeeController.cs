using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Models.Request;
using PPC.TrainingDevelopment.Api.Services.Interfaces;

namespace PPC.TrainingDevelopment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all employee records
        /// </summary>
        /// <returns>List of all employee records</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee by personnel number
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <returns>Employee record</returns>
        [HttpGet("{personnelNumber}")]
        public async Task<ActionResult<Employee>> GetByPersonnelNumber(string personnelNumber)
        {
            try
            {
                var employee = await _employeeService.GetByPersonnelNumberAsync(personnelNumber);
                if (employee == null)
                {
                    return NotFound(new { message = $"Employee with personnel number {personnelNumber} not found." });
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the employee record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Search employee records by name, personnel number, job title, job grade, ID number, or site
        /// </summary>
        /// <param name="searchTerm">Search term to match against first name, last name, known name, personnel number, job title, job grade, ID number, or site</param>
        /// <returns>List of matching employee records</returns>
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest(new { message = "Search term cannot be empty." });
                }

                var employees = await _employeeService.SearchAsync(searchTerm);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching employee records.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee records by race
        /// </summary>
        /// <param name="race">Race category</param>
        /// <returns>List of employee records for the specified race</returns>
        [HttpGet("race/{race}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetByRace(string race)
        {
            try
            {
                var employees = await _employeeService.GetByRaceAsync(race);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records by race.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee records by gender
        /// </summary>
        /// <param name="gender">Gender category</param>
        /// <returns>List of employee records for the specified gender</returns>
        [HttpGet("gender/{gender}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetByGender(string gender)
        {
            try
            {
                var employees = await _employeeService.GetByGenderAsync(gender);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records by gender.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee records by Employment Equity level
        /// </summary>
        /// <param name="eeLevel">Employment Equity level</param>
        /// <returns>List of employee records for the specified EE level</returns>
        [HttpGet("ee-level/{eeLevel}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetByEELevel(string eeLevel)
        {
            try
            {
                var employees = await _employeeService.GetByEELevelAsync(eeLevel);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records by EE level.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee records by Employment Equity category
        /// </summary>
        /// <param name="eeCategory">Employment Equity category</param>
        /// <returns>List of employee records for the specified EE category</returns>
        [HttpGet("ee-category/{eeCategory}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetByEECategory(string eeCategory)
        {
            try
            {
                var employees = await _employeeService.GetByEECategoryAsync(eeCategory);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records by EE category.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee records by disability status
        /// </summary>
        /// <param name="hasDisability">Disability status (true/false)</param>
        /// <returns>List of employee records for the specified disability status</returns>
        [HttpGet("disability/{hasDisability}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetWithDisability(bool hasDisability)
        {
            try
            {
                var employees = await _employeeService.GetWithDisabilityAsync(hasDisability);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records by disability status.", error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new employee record
        /// </summary>
        /// <param name="request">Create employee request</param>
        /// <returns>Created employee record</returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> Create([FromBody] CreateEmployeeRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if employee already exists
                var existingEmployee = await _employeeService.ExistsAsync(request.PersonnelNumber);
                if (existingEmployee)
                {
                    return BadRequest(new { message = $"Employee with personnel number {request.PersonnelNumber} already exists." });
                }

                var employee = new Employee
                {
                    PersonnelNumber = request.PersonnelNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    KnownName = request.KnownName,
                    Initials = request.Initials,
                    Race = request.Race,
                    Gender = request.Gender,
                    Disability = request.Disability,
                    EELevel = request.EELevel,
                    EECategory = request.EECategory,
                    JobTitle = request.JobTitle,
                    JobGrade = request.JobGrade,
                    IDNumber = request.IDNumber,
                    Site = request.Site,
                    HighestQualification = request.HighestQualification
                };

                var createdEmployee = await _employeeService.CreateAsync(employee);
                return CreatedAtAction(nameof(GetByPersonnelNumber), new { personnelNumber = createdEmployee.PersonnelNumber }, createdEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the employee record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing employee record
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <param name="request">Update employee request</param>
        /// <returns>Updated employee record</returns>
        [HttpPut("{personnelNumber}")]
        public async Task<ActionResult<Employee>> Update(string personnelNumber, [FromBody] UpdateEmployeeRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = new Employee
                {
                    PersonnelNumber = personnelNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    KnownName = request.KnownName,
                    Initials = request.Initials,
                    Race = request.Race,
                    Gender = request.Gender,
                    Disability = request.Disability,
                    EELevel = request.EELevel,
                    EECategory = request.EECategory,
                    JobTitle = request.JobTitle,
                    JobGrade = request.JobGrade,
                    IDNumber = request.IDNumber,
                    Site = request.Site,
                    HighestQualification = request.HighestQualification
                };

                var updatedEmployee = await _employeeService.UpdateAsync(personnelNumber, employee);
                if (updatedEmployee == null)
                {
                    return NotFound(new { message = $"Employee with personnel number {personnelNumber} not found." });
                }

                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the employee record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee records by job title
        /// </summary>
        /// <param name="jobTitle">Job title</param>
        /// <returns>List of employee records for the specified job title</returns>
        [HttpGet("job-title/{jobTitle}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetByJobTitle(string jobTitle)
        {
            try
            {
                var employees = await _employeeService.GetByJobTitleAsync(jobTitle);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records by job title.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee records by job grade
        /// </summary>
        /// <param name="jobGrade">Job grade</param>
        /// <returns>List of employee records for the specified job grade</returns>
        [HttpGet("job-grade/{jobGrade}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetByJobGrade(string jobGrade)
        {
            try
            {
                var employees = await _employeeService.GetByJobGradeAsync(jobGrade);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records by job grade.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee records by site
        /// </summary>
        /// <param name="site">Site</param>
        /// <returns>List of employee records for the specified site</returns>
        [HttpGet("site/{site}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetBySite(string site)
        {
            try
            {
                var employees = await _employeeService.GetBySiteAsync(site);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee records by site.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee by ID number
        /// </summary>
        /// <param name="idNumber">ID number</param>
        /// <returns>Employee record</returns>
        [HttpGet("id-number/{idNumber}")]
        public async Task<ActionResult<Employee>> GetByIDNumber(string idNumber)
        {
            try
            {
                var employee = await _employeeService.GetByIDNumberAsync(idNumber);
                if (employee == null)
                {
                    return NotFound(new { message = $"Employee with ID number {idNumber} not found." });
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the employee record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete an employee record
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <returns>Success/failure result</returns>
        [HttpDelete("{personnelNumber}")]
        public async Task<IActionResult> Delete(string personnelNumber)
        {
            try
            {
                var result = await _employeeService.DeleteAsync(personnelNumber);
                if (!result)
                {
                    return NotFound(new { message = $"Employee with personnel number {personnelNumber} not found." });
                }

                return Ok(new { message = "Employee record deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the employee record.", error = ex.Message });
            }
        }
    }
}