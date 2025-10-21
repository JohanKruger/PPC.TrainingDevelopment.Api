namespace PPC.TrainingDevelopment.Api.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string?> LoginAsync(string username, string password);
    }
}
