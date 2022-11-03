using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using SportCity.Core.Guards;
using SportCity.Core.Interfaces;
using SportCity.Infrastructure.Guards;

namespace SportCity.Infrastructure.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenClaimsService _tokenClaimService;
    private readonly UserManager<EfApplicationUser> _userManager;


    public AuthenticationService(ITokenClaimsService tokenClaimService, UserManager<EfApplicationUser> userManager)
    {
        _tokenClaimService = tokenClaimService;
        _userManager = userManager;
    }

    public async Task<string> Authenticate(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        Guard.Against.EntityNotFound(user, nameof(email), email);
        var result = await _userManager.CheckPasswordAsync(user, password);
        Guard.Against.WrongPassword(result);
        var token = await _tokenClaimService.GetTokenAsync(user.Id);
        return token;
    }
}
