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
    public class TrainingRecordEventController : ControllerBase
    {
        private readonly ITrainingRecordEventService _trainingRecordEventService;

        public TrainingRecordEventController(ITrainingRecordEventService trainingRecordEventService)
        {
            _trainingRecordEventService = trainingRecordEventService;
        }

        /// <summary>
        /// Get all training record event records
        /// </summary>
        /// <returns>List of all training record event records</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingRecordEvent>>> GetAll()
        {
            try
            {
                var trainingRecordEvents = await _trainingRecordEventService.GetAllAsync();
                return Ok(trainingRecordEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training record event records.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training record event by ID
        /// </summary>
        /// <param name="trainingRecordEventId">Training record event ID</param>
        /// <returns>Training record event record</returns>
        [HttpGet("{trainingRecordEventId}")]
        public async Task<ActionResult<TrainingRecordEvent>> GetById(int trainingRecordEventId)
        {
            try
            {
                var trainingRecordEvent = await _trainingRecordEventService.GetByIdAsync(trainingRecordEventId);
                if (trainingRecordEvent == null)
                {
                    return NotFound(new { message = $"Training record event with ID {trainingRecordEventId} not found." });
                }
                return Ok(trainingRecordEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the training record event record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training record events by training event ID
        /// </summary>
        /// <param name="trainingEventId">Training event ID</param>
        /// <returns>List of training record events for the training event</returns>
        [HttpGet("training-event/{trainingEventId}")]
        public async Task<ActionResult<IEnumerable<TrainingRecordEvent>>> GetByTrainingEventId(int trainingEventId)
        {
            try
            {
                var trainingRecordEvents = await _trainingRecordEventService.GetByTrainingEventIdAsync(trainingEventId);
                return Ok(trainingRecordEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training record events by training event ID.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training record events by personnel number
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <returns>List of training record events for the employee</returns>
        [HttpGet("personnel/{personnelNumber}")]
        public async Task<ActionResult<IEnumerable<TrainingRecordEvent>>> GetByPersonnelNumber(string personnelNumber)
        {
            try
            {
                var trainingRecordEvents = await _trainingRecordEventService.GetByPersonnelNumberAsync(personnelNumber);
                return Ok(trainingRecordEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training record events by personnel number.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training record events by date range
        /// </summary>
        /// <param name="startDate">Start date (yyyy-MM-dd)</param>
        /// <param name="endDate">End date (yyyy-MM-dd)</param>
        /// <returns>List of training record events within the date range</returns>
        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<TrainingRecordEvent>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                if (endDate < startDate)
                {
                    return BadRequest(new { message = "End date must be greater than or equal to start date." });
                }

                var trainingRecordEvents = await _trainingRecordEventService.GetByDateRangeAsync(startDate, endDate);
                return Ok(trainingRecordEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training record events by date range.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training record events with evidence
        /// </summary>
        /// <returns>List of training record events that have evidence</returns>
        [HttpGet("with-evidence")]
        public async Task<ActionResult<IEnumerable<TrainingRecordEvent>>> GetWithEvidence()
        {
            try
            {
                var trainingRecordEvents = await _trainingRecordEventService.GetWithEvidenceAsync();
                return Ok(trainingRecordEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training record events with evidence.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training record events without evidence
        /// </summary>
        /// <returns>List of training record events that do not have evidence</returns>
        [HttpGet("without-evidence")]
        public async Task<ActionResult<IEnumerable<TrainingRecordEvent>>> GetWithoutEvidence()
        {
            try
            {
                var trainingRecordEvents = await _trainingRecordEventService.GetWithoutEvidenceAsync();
                return Ok(trainingRecordEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training record events without evidence.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get total costs by training event ID
        /// </summary>
        /// <param name="trainingEventId">Training event ID</param>
        /// <returns>Total costs for all training record events of the training event</returns>
        [HttpGet("costs/training-event/{trainingEventId}")]
        public async Task<ActionResult<decimal>> GetTotalCostsByTrainingEventId(int trainingEventId)
        {
            try
            {
                var totalCosts = await _trainingRecordEventService.GetTotalCostsByTrainingEventIdAsync(trainingEventId);
                return Ok(new { trainingEventId, totalCosts });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while calculating total costs by training event ID.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get total costs by personnel number
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <returns>Total costs for all training record events of the employee</returns>
        [HttpGet("costs/personnel/{personnelNumber}")]
        public async Task<ActionResult<decimal>> GetTotalCostsByPersonnelNumber(string personnelNumber)
        {
            try
            {
                var totalCosts = await _trainingRecordEventService.GetTotalCostsByPersonnelNumberAsync(personnelNumber);
                return Ok(new { personnelNumber, totalCosts });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while calculating total costs by personnel number.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get total costs by date range
        /// </summary>
        /// <param name="startDate">Start date (yyyy-MM-dd)</param>
        /// <param name="endDate">End date (yyyy-MM-dd)</param>
        /// <returns>Total costs for all training record events within the date range</returns>
        [HttpGet("costs/date-range")]
        public async Task<ActionResult<decimal>> GetTotalCostsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                if (endDate < startDate)
                {
                    return BadRequest(new { message = "End date must be greater than or equal to start date." });
                }

                var totalCosts = await _trainingRecordEventService.GetTotalCostsByDateRangeAsync(startDate, endDate);
                return Ok(new { startDate, endDate, totalCosts });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while calculating total costs by date range.", error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new training record event record
        /// </summary>
        /// <param name="request">Create training record event request</param>
        /// <returns>Created training record event record</returns>
        [HttpPost]
        public async Task<ActionResult<TrainingRecordEvent>> Create([FromBody] CreateTrainingRecordEventRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var trainingRecordEvent = new TrainingRecordEvent
                {
                    TrainingEventId = request.TrainingEventId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Hours = request.Hours,
                    Minutes = request.Minutes,
                    PersonnelNumber = request.PersonnelNumber,
                    Evidence = request.Evidence,
                    ServiceProviderExternal = request.ServiceProviderExternal,
                    CostTrainingMaterials = request.CostTrainingMaterials,
                    CostTrainers = request.CostTrainers,
                    CostTrainingFacilities = request.CostTrainingFacilities,
                    ScholarshipsBursaries = request.ScholarshipsBursaries,
                    CourseFees = request.CourseFees,
                    Accommodation = request.Accommodation,
                    Travel = request.Travel,
                    Meal = request.Meal,
                    AdministrationCosts = request.AdministrationCosts,
                    EquipmentDepreciation = request.EquipmentDepreciation
                };

                var createdTrainingRecordEvent = await _trainingRecordEventService.CreateAsync(trainingRecordEvent);
                return CreatedAtAction(nameof(GetById), new { trainingRecordEventId = createdTrainingRecordEvent.TrainingRecordEventId }, createdTrainingRecordEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the training record event record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing training record event record
        /// </summary>
        /// <param name="trainingRecordEventId">Training record event ID</param>
        /// <param name="request">Update training record event request</param>
        /// <returns>Updated training record event record</returns>
        [HttpPut("{trainingRecordEventId}")]
        public async Task<ActionResult<TrainingRecordEvent>> Update(int trainingRecordEventId, [FromBody] UpdateTrainingRecordEventRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var trainingRecordEvent = new TrainingRecordEvent
                {
                    TrainingRecordEventId = trainingRecordEventId,
                    TrainingEventId = request.TrainingEventId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Hours = request.Hours,
                    Minutes = request.Minutes,
                    PersonnelNumber = request.PersonnelNumber,
                    Evidence = request.Evidence,
                    ServiceProviderExternal = request.ServiceProviderExternal,
                    CostTrainingMaterials = request.CostTrainingMaterials,
                    CostTrainers = request.CostTrainers,
                    CostTrainingFacilities = request.CostTrainingFacilities,
                    ScholarshipsBursaries = request.ScholarshipsBursaries,
                    CourseFees = request.CourseFees,
                    Accommodation = request.Accommodation,
                    Travel = request.Travel,
                    Meal = request.Meal,
                    AdministrationCosts = request.AdministrationCosts,
                    EquipmentDepreciation = request.EquipmentDepreciation
                };

                var updatedTrainingRecordEvent = await _trainingRecordEventService.UpdateAsync(trainingRecordEventId, trainingRecordEvent);
                if (updatedTrainingRecordEvent == null)
                {
                    return NotFound(new { message = $"Training record event with ID {trainingRecordEventId} not found." });
                }

                return Ok(updatedTrainingRecordEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the training record event record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a training record event record
        /// </summary>
        /// <param name="trainingRecordEventId">Training record event ID</param>
        /// <returns>Success/failure result</returns>
        [HttpDelete("{trainingRecordEventId}")]
        public async Task<IActionResult> Delete(int trainingRecordEventId)
        {
            try
            {
                var result = await _trainingRecordEventService.DeleteAsync(trainingRecordEventId);
                if (!result)
                {
                    return NotFound(new { message = $"Training record event with ID {trainingRecordEventId} not found." });
                }

                return Ok(new { message = "Training record event record deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the training record event record.", error = ex.Message });
            }
        }
    }
}