
using PPC.TrainingDevelopment.Api.Services.Interfaces;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace PPC.TrainingDevelopment.Api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string?> LoginAsync(string username, string password)
        {
            try
            {
                bool isValid = true;
                // Validate credentials against Active Directory
#if !DEBUG
                using var context = new PrincipalContext(ContextType.Domain, "ppc.africa");
                isValid = context.ValidateCredentials(username, password);
#endif

                if (isValid)
                {
                    // Generate JWT token
                    var token = GenerateJwtToken(username);
                    return Task.FromResult<string?>(token);
                }

                return Task.FromResult<string?>(null);
            }
            catch (Exception)
            {
                // Log exception here if needed
                return Task.FromResult<string?>(null);
            }
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? "CC296875-0C26-47CE-A691-8BE984D5AF3Bz";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
