using Ardalis.GuardClauses;
using SportCity.Core.Interfaces;
using SportCity.SharedKernel;

namespace SportCity.Infrastructure.Identity.Services;

public class AuthorizationService : IAuthorizationService
{
  private readonly ITokenClaimsService _tokenClaimService;
  private string _token; 
  
  public AuthorizationService(ITokenClaimsService tokenClaimService)
  {
    _tokenClaimService = tokenClaimService;
  }

  public bool IsAdmin()
  {
    var roles = _tokenClaimService.GetUserRoles(_token);
    return roles.Contains(Roles.Admin);
  }
  
  public void SetToken(string token) => _token = Guard.Against.NullOrWhiteSpace(token, nameof(token));
}
