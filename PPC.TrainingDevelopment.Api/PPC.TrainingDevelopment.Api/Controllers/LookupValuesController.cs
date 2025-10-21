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
    public class LookupValuesController : ControllerBase
    {
        private readonly ILookupValueService _lookupValueService;

        public LookupValuesController(ILookupValueService lookupValueService)
        {
            _lookupValueService = lookupValueService;
        }

        /// <summary>
        /// Get all lookup values
        /// </summary>
        /// <returns>List of all lookup values</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LookupValue>>> GetAll()
        {
            try
            {
                var lookupValues = await _lookupValueService.GetAllAsync();
                return Ok(lookupValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving lookup values.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get lookup value by ID
        /// </summary>
        /// <param name="id">Lookup value ID</param>
        /// <returns>Lookup value</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LookupValue>> GetById(int id)
        {
            try
            {
                var lookupValue = await _lookupValueService.GetByIdAsync(id);
                if (lookupValue == null)
                {
                    return NotFound(new { message = $"Lookup value with ID {id} not found." });
                }
                return Ok(lookupValue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the lookup value.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get lookup values by type
        /// </summary>
        /// <param name="type">Lookup type</param>
        /// <returns>List of lookup values for the specified type</returns>
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<LookupValue>>> GetByType(string type)
        {
            try
            {
                var lookupValues = await _lookupValueService.GetByTypeAsync(type);
                return Ok(lookupValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving lookup values by type.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get active lookup values by type
        /// </summary>
        /// <param name="type">Lookup type</param>
        /// <returns>List of active lookup values for the specified type</returns>
        [HttpGet("type/{type}/active")]
        public async Task<ActionResult<IEnumerable<LookupValue>>> GetActiveByType(string type)
        {
            try
            {
                var lookupValues = await _lookupValueService.GetActiveByTypeAsync(type);
                return Ok(lookupValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving active lookup values by type.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get children of a parent lookup value
        /// </summary>
        /// <param name="parentId">Parent lookup value ID</param>
        /// <returns>List of child lookup values</returns>
        [HttpGet("parent/{parentId}/children")]
        public async Task<ActionResult<IEnumerable<LookupValue>>> GetChildren(int parentId)
        {
            try
            {
                var children = await _lookupValueService.GetChildrenAsync(parentId);
                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving child lookup values.", error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new lookup value
        /// </summary>
        /// <param name="request">Create lookup value request</param>
        /// <returns>Created lookup value</returns>
        [HttpPost]
        public async Task<ActionResult<LookupValue>> Create([FromBody] CreateLookupValueRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate parent exists if ParentId is provided
                if (request.ParentId.HasValue)
                {
                    var parentExists = await _lookupValueService.ExistsAsync(request.ParentId.Value);
                    if (!parentExists)
                    {
                        return BadRequest(new { message = $"Parent lookup value with ID {request.ParentId} does not exist." });
                    }
                }

                var lookupValue = new LookupValue
                {
                    LookupType = request.LookupType,
                    Value = request.Value,
                    Code = request.Code,
                    ParentId = request.ParentId,
                    SortOrder = request.SortOrder,
                    IsActive = request.IsActive
                };

                var createdLookupValue = await _lookupValueService.CreateAsync(lookupValue);
                return CreatedAtAction(nameof(GetById), new { id = createdLookupValue.LookupId }, createdLookupValue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the lookup value.", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing lookup value
        /// </summary>
        /// <param name="id">Lookup value ID</param>
        /// <param name="request">Update lookup value request</param>
        /// <returns>Updated lookup value</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<LookupValue>> Update(int id, [FromBody] UpdateLookupValueRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate parent exists if ParentId is provided and it's not the same as the current ID
                if (request.ParentId.HasValue)
                {
                    if (request.ParentId.Value == id)
                    {
                        return BadRequest(new { message = "A lookup value cannot be its own parent." });
                    }

                    var parentExists = await _lookupValueService.ExistsAsync(request.ParentId.Value);
                    if (!parentExists)
                    {
                        return BadRequest(new { message = $"Parent lookup value with ID {request.ParentId} does not exist." });
                    }
                }

                var lookupValue = new LookupValue
                {
                    LookupType = request.LookupType,
                    Value = request.Value,
                    Code = request.Code,
                    ParentId = request.ParentId,
                    SortOrder = request.SortOrder,
                    IsActive = request.IsActive
                };

                var updatedLookupValue = await _lookupValueService.UpdateAsync(id, lookupValue);
                if (updatedLookupValue == null)
                {
                    return NotFound(new { message = $"Lookup value with ID {id} not found." });
                }

                return Ok(updatedLookupValue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the lookup value.", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a lookup value
        /// </summary>
        /// <param name="id">Lookup value ID</param>
        /// <returns>Success/failure result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _lookupValueService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Lookup value with ID {id} not found or cannot be deleted because it has child records." });
                }

                return Ok(new { message = "Lookup value deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the lookup value.", error = ex.Message });
            }
        }
    }
}