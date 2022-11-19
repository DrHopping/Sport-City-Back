namespace SportCity.Core.Interfaces;

public interface IAuthenticationService
{
    Task<(string Id, string Token, string Role)> Authenticate(string email, string password);
}
