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
    public class TrainingEventController : ControllerBase
    {
        private readonly ITrainingEventService _trainingEventService;

        public TrainingEventController(ITrainingEventService trainingEventService)
        {
            _trainingEventService = trainingEventService;
        }

        /// <summary>
        /// Get all training event records
        /// </summary>
        /// <returns>List of all training event records</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> GetAll()
        {
            try
            {
                var trainingEvents = await _trainingEventService.GetAllAsync();
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training event records.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training event by ID
        /// </summary>
        /// <param name="trainingEventId">Training event ID</param>
        /// <returns>Training event record</returns>
        [HttpGet("{trainingEventId}")]
        public async Task<ActionResult<TrainingEvent>> GetById(int trainingEventId)
        {
            try
            {
                var trainingEvent = await _trainingEventService.GetByIdAsync(trainingEventId);
                if (trainingEvent == null)
                {
                    return NotFound(new { message = $"Training event with ID {trainingEventId} not found." });
                }
                return Ok(trainingEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the training event record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training events by personnel number
        /// </summary>
        /// <param name="personnelNumber">Personnel number</param>
        /// <returns>List of training events for the employee</returns>
        [HttpGet("employee/{personnelNumber}")]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> GetByPersonnelNumber(string personnelNumber)
        {
            try
            {
                var trainingEvents = await _trainingEventService.GetByPersonnelNumberAsync(personnelNumber);
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training events by personnel number.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training events by ID number (non-employee)
        /// </summary>
        /// <param name="idNumber">South African ID number</param>
        /// <returns>List of training events for the non-employee</returns>
        [HttpGet("non-employee/{idNumber}")]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> GetByIDNumber(string idNumber)
        {
            try
            {
                var trainingEvents = await _trainingEventService.GetByIDNumberAsync(idNumber);
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training events by ID number.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training events by event type ID
        /// </summary>
        /// <param name="eventTypeId">Event type ID</param>
        /// <returns>List of training events for the specified event type</returns>
        [HttpGet("event-type/{eventTypeId}")]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> GetByEventType(int eventTypeId)
        {
            try
            {
                var trainingEvents = await _trainingEventService.GetByEventTypeAsync(eventTypeId);
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training events by event type.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training events by region
        /// </summary>
        /// <param name="regionId">Region ID</param>
        /// <returns>List of training events for the specified region</returns>
        [HttpGet("region/{regionId}")]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> GetByRegion(int regionId)
        {
            try
            {
                var trainingEvents = await _trainingEventService.GetByRegionAsync(regionId);
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training events by region.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training events by province
        /// </summary>
        /// <param name="provinceId">Province ID</param>
        /// <returns>List of training events for the specified province</returns>
        [HttpGet("province/{provinceId}")]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> GetByProvince(int provinceId)
        {
            try
            {
                var trainingEvents = await _trainingEventService.GetByProvinceAsync(provinceId);
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training events by province.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training events by municipality
        /// </summary>
        /// <param name="municipalityId">Municipality ID</param>
        /// <returns>List of training events for the specified municipality</returns>
        [HttpGet("municipality/{municipalityId}")]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> GetByMunicipality(int municipalityId)
        {
            try
            {
                var trainingEvents = await _trainingEventService.GetByMunicipalityAsync(municipalityId);
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training events by municipality.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get training events by site
        /// </summary>
        /// <param name="siteId">Site ID</param>
        /// <returns>List of training events for the specified site</returns>
        [HttpGet("site/{siteId}")]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> GetBySite(int siteId)
        {
            try
            {
                var trainingEvents = await _trainingEventService.GetBySiteAsync(siteId);
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving training events by site.", error = ex.Message });
            }
        }

        /// <summary>
        /// Search training events by training event name or event type
        /// </summary>
        /// <param name="searchTerm">Search term to match against training event name or event type</param>
        /// <returns>List of matching training event records</returns>
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<TrainingEvent>>> Search(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest(new { message = "Search term cannot be empty." });
                }

                var trainingEvents = await _trainingEventService.SearchByTrainingEventNameAsync(searchTerm);
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching training event records.", error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new training event record
        /// </summary>
        /// <param name="request">Create training event request</param>
        /// <returns>Created training event record</returns>
        [HttpPost]
        public async Task<ActionResult<TrainingEvent>> Create([FromBody] CreateTrainingEventRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var trainingEvent = new TrainingEvent
                {
                    PersonnelNumber = request.PersonnelNumber,
                    IDNumber = request.IDNumber,
                    EventTypeId = request.EventTypeId,
                    TrainingEventNameId = request.TrainingEventNameId,
                    RegionId = request.RegionId,
                    ProvinceId = request.ProvinceId,
                    MunicipalityId = request.MunicipalityId,
                    SiteId = request.SiteId
                };

                var createdTrainingEvent = await _trainingEventService.CreateAsync(trainingEvent);
                return CreatedAtAction(nameof(GetById), new { trainingEventId = createdTrainingEvent.TrainingEventId }, createdTrainingEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the training event record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing training event record
        /// </summary>
        /// <param name="trainingEventId">Training event ID</param>
        /// <param name="request">Update training event request</param>
        /// <returns>Updated training event record</returns>
        [HttpPut("{trainingEventId}")]
        public async Task<ActionResult<TrainingEvent>> Update(int trainingEventId, [FromBody] UpdateTrainingEventRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var trainingEvent = new TrainingEvent
                {
                    TrainingEventId = trainingEventId,
                    PersonnelNumber = request.PersonnelNumber,
                    IDNumber = request.IDNumber,
                    EventTypeId = request.EventTypeId,
                    TrainingEventNameId = request.TrainingEventNameId,
                    RegionId = request.RegionId,
                    ProvinceId = request.ProvinceId,
                    MunicipalityId = request.MunicipalityId,
                    SiteId = request.SiteId
                };

                var updatedTrainingEvent = await _trainingEventService.UpdateAsync(trainingEventId, trainingEvent);
                if (updatedTrainingEvent == null)
                {
                    return NotFound(new { message = $"Training event with ID {trainingEventId} not found." });
                }

                return Ok(updatedTrainingEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the training event record.", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a training event record
        /// </summary>
        /// <param name="trainingEventId">Training event ID</param>
        /// <returns>Success/failure result</returns>
        [HttpDelete("{trainingEventId}")]
        public async Task<IActionResult> Delete(int trainingEventId)
        {
            try
            {
                var result = await _trainingEventService.DeleteAsync(trainingEventId);
                if (!result)
                {
                    return NotFound(new { message = $"Training event with ID {trainingEventId} not found." });
                }

                return Ok(new { message = "Training event record deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the training event record.", error = ex.Message });
            }
        }
    }
}