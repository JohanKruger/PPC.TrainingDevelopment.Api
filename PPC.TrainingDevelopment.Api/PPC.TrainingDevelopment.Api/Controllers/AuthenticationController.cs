using Microsoft.AspNetCore.Mvc;
using PPC.TrainingDevelopment.Api.Services.Interfaces;
using PPC.TrainingDevelopment.Api.Models.Request;

namespace PPC.TrainingDevelopment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var token = await _authenticationService.LoginAsync(request.Username, request.Password);

            if (token != null)
            {
                return Ok(new { token = token, message = "Login successful" });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
}
