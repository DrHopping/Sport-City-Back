namespace SportCity.Core.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string id);
    string GetUserIdentity(string token);
    List<string> GetUserRoles(string token);
}
