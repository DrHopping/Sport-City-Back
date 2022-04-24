namespace SportCity.Core.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}
