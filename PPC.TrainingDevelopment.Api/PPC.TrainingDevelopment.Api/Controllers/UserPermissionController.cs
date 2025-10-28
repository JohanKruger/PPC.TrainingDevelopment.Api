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
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionService _userPermissionService;

        public UserPermissionController(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        /// <summary>
        /// Get all user permissions
        /// </summary>
        /// <returns>List of all user permissions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPermission>>> GetAll()
        {
            try
            {
                var permissions = await _userPermissionService.GetAllAsync();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving user permissions.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get user permission by ID
        /// </summary>
        /// <param name="id">Permission ID</param>
        /// <returns>User permission record</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserPermission>> GetById(int id)
        {
            try
            {
                var permission = await _userPermissionService.GetByIdAsync(id);
                if (permission == null)
                {
                    return NotFound(new { message = $"User permission with ID {id} not found." });
                }
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the user permission.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get user permissions by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>List of user permissions for the specified username</returns>
        [HttpGet("username/{username}")]
        public async Task<ActionResult<IEnumerable<UserPermission>>> GetByUsername(string username)
        {
            try
            {
                var permissions = await _userPermissionService.GetByUsernameAsync(username);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving user permissions.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get user permissions by permission code
        /// </summary>
        /// <param name="permissionCode">Permission code</param>
        /// <returns>List of user permissions with the specified permission code</returns>
        [HttpGet("permission-code/{permissionCode}")]
        public async Task<ActionResult<IEnumerable<UserPermission>>> GetByPermissionCode(string permissionCode)
        {
            try
            {
                var permissions = await _userPermissionService.GetByPermissionCodeAsync(permissionCode);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving user permissions.", error = ex.Message });
            }
        }

        /// <summary>
        /// Check if a user has a specific permission
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="permissionCode">Permission code</param>
        /// <returns>Boolean indicating if the permission exists</returns>
        [HttpGet("check/{username}/{permissionCode}")]
        public async Task<ActionResult<bool>> HasPermission(string username, string permissionCode)
        {
            try
            {
                var hasPermission = await _userPermissionService.HasPermissionAsync(username, permissionCode);
                return Ok(new { hasPermission });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while checking the permission.", error = ex.Message });
            }
        }

        /// <summary>
        /// Search user permissions by search term
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <returns>List of matching user permissions</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<UserPermission>>> Search([FromQuery] string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest(new { message = "Search term cannot be empty." });
                }

                var permissions = await _userPermissionService.SearchAsync(searchTerm);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching user permissions.", error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new user permission
        /// </summary>
        /// <param name="request">Create user permission request</param>
        /// <returns>Created user permission</returns>
        [HttpPost]
        public async Task<ActionResult<UserPermission>> Create([FromBody] CreateUserPermissionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the permission already exists
                var existingPermission = await _userPermissionService.HasPermissionAsync(request.Username, request.PermissionCode);
                if (existingPermission)
                {
                    return Conflict(new { message = $"Permission '{request.PermissionCode}' already exists for user {request.Username}." });
                }

                var userPermission = new UserPermission
                {
                    Username = request.Username,
                    PermissionCode = request.PermissionCode
                };

                var createdPermission = await _userPermissionService.CreateAsync(userPermission);
                return CreatedAtAction(nameof(GetById), new { id = createdPermission.PermissionId }, createdPermission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user permission.", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing user permission
        /// </summary>
        /// <param name="id">Permission ID</param>
        /// <param name="request">Update user permission request</param>
        /// <returns>Updated user permission</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserPermission>> Update(int id, [FromBody] UpdateUserPermissionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userPermission = new UserPermission
                {
                    Username = request.Username,
                    PermissionCode = request.PermissionCode
                };

                var updatedPermission = await _userPermissionService.UpdateAsync(id, userPermission);
                if (updatedPermission == null)
                {
                    return NotFound(new { message = $"User permission with ID {id} not found." });
                }

                return Ok(updatedPermission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the user permission.", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a user permission
        /// </summary>
        /// <param name="id">Permission ID</param>
        /// <returns>Success message</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _userPermissionService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound(new { message = $"User permission with ID {id} not found." });
                }

                return Ok(new { message = "User permission deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the user permission.", error = ex.Message });
            }
        }
    }
}