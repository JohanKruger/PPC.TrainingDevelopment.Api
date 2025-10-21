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
    public class EmployeeLookupController : ControllerBase
    {
        private readonly IEmployeeLookupService _employeeLookupService;

        public EmployeeLookupController(IEmployeeLookupService employeeLookupService)
        {
            _employeeLookupService = employeeLookupService;
        }

        /// <summary>
        /// Get all employee lookup records
        /// </summary>
        /// <returns>List of all employee lookup records</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeLookup>>> GetAll()
        {
            try
            {
                var employees = await _employeeLookupService.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee lookup records.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee lookup by personnel number
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <returns>Employee lookup record</returns>
        [HttpGet("{personnelNumber}")]
        public async Task<ActionResult<EmployeeLookup>> GetByPersonnelNumber(string personnelNumber)
        {
            try
            {
                var employee = await _employeeLookupService.GetByPersonnelNumberAsync(personnelNumber);
                if (employee == null)
                {
                    return NotFound(new { message = $"Employee with personnel number {personnelNumber} not found." });
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the employee lookup record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Search employee lookup records by name or personnel number
        /// </summary>
        /// <param name="searchTerm">Search term to match against first name, last name, known name, or personnel number</param>
        /// <returns>List of matching employee lookup records</returns>
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<EmployeeLookup>>> Search(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest(new { message = "Search term cannot be empty." });
                }

                var employees = await _employeeLookupService.SearchAsync(searchTerm);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching employee lookup records.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee lookup records by race
        /// </summary>
        /// <param name="race">Race category</param>
        /// <returns>List of employee lookup records for the specified race</returns>
        [HttpGet("race/{race}")]
        public async Task<ActionResult<IEnumerable<EmployeeLookup>>> GetByRace(string race)
        {
            try
            {
                var employees = await _employeeLookupService.GetByRaceAsync(race);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee lookup records by race.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee lookup records by gender
        /// </summary>
        /// <param name="gender">Gender category</param>
        /// <returns>List of employee lookup records for the specified gender</returns>
        [HttpGet("gender/{gender}")]
        public async Task<ActionResult<IEnumerable<EmployeeLookup>>> GetByGender(string gender)
        {
            try
            {
                var employees = await _employeeLookupService.GetByGenderAsync(gender);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee lookup records by gender.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee lookup records by Employment Equity level
        /// </summary>
        /// <param name="eeLevel">Employment Equity level</param>
        /// <returns>List of employee lookup records for the specified EE level</returns>
        [HttpGet("ee-level/{eeLevel}")]
        public async Task<ActionResult<IEnumerable<EmployeeLookup>>> GetByEELevel(string eeLevel)
        {
            try
            {
                var employees = await _employeeLookupService.GetByEELevelAsync(eeLevel);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee lookup records by EE level.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee lookup records by Employment Equity category
        /// </summary>
        /// <param name="eeCategory">Employment Equity category</param>
        /// <returns>List of employee lookup records for the specified EE category</returns>
        [HttpGet("ee-category/{eeCategory}")]
        public async Task<ActionResult<IEnumerable<EmployeeLookup>>> GetByEECategory(string eeCategory)
        {
            try
            {
                var employees = await _employeeLookupService.GetByEECategoryAsync(eeCategory);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee lookup records by EE category.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee lookup records by disability status
        /// </summary>
        /// <param name="hasDisability">Disability status (true/false)</param>
        /// <returns>List of employee lookup records for the specified disability status</returns>
        [HttpGet("disability/{hasDisability}")]
        public async Task<ActionResult<IEnumerable<EmployeeLookup>>> GetWithDisability(bool hasDisability)
        {
            try
            {
                var employees = await _employeeLookupService.GetWithDisabilityAsync(hasDisability);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving employee lookup records by disability status.", error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new employee lookup record
        /// </summary>
        /// <param name="request">Create employee lookup request</param>
        /// <returns>Created employee lookup record</returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeLookup>> Create([FromBody] CreateEmployeeLookupRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if employee already exists
                var existingEmployee = await _employeeLookupService.ExistsAsync(request.PersonnelNumber);
                if (existingEmployee)
                {
                    return BadRequest(new { message = $"Employee with personnel number {request.PersonnelNumber} already exists." });
                }

                var employeeLookup = new EmployeeLookup
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
                    EECategory = request.EECategory
                };

                var createdEmployee = await _employeeLookupService.CreateAsync(employeeLookup);
                return CreatedAtAction(nameof(GetByPersonnelNumber), new { personnelNumber = createdEmployee.PersonnelNumber }, createdEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the employee lookup record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing employee lookup record
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <param name="request">Update employee lookup request</param>
        /// <returns>Updated employee lookup record</returns>
        [HttpPut("{personnelNumber}")]
        public async Task<ActionResult<EmployeeLookup>> Update(string personnelNumber, [FromBody] UpdateEmployeeLookupRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employeeLookup = new EmployeeLookup
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
                    EECategory = request.EECategory
                };

                var updatedEmployee = await _employeeLookupService.UpdateAsync(personnelNumber, employeeLookup);
                if (updatedEmployee == null)
                {
                    return NotFound(new { message = $"Employee with personnel number {personnelNumber} not found." });
                }

                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the employee lookup record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete an employee lookup record
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <returns>Success/failure result</returns>
        [HttpDelete("{personnelNumber}")]
        public async Task<IActionResult> Delete(string personnelNumber)
        {
            try
            {
                var result = await _employeeLookupService.DeleteAsync(personnelNumber);
                if (!result)
                {
                    return NotFound(new { message = $"Employee with personnel number {personnelNumber} not found." });
                }

                return Ok(new { message = "Employee lookup record deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the employee lookup record.", error = ex.Message });
            }
        }
    }
}