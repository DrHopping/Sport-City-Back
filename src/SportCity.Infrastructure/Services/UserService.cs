using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using SportCity.Core.Interfaces;
using SportCity.Infrastructure.Guards;
using SportCity.Infrastructure.Identity;

namespace SportCity.Infrastructure.Services;

public class UserService
{
  private readonly UserManager<EfApplicationUser> _userManager;
  private readonly ITokenClaimsService _tokenClaimService;

  public UserService(UserManager<EfApplicationUser> userManager, ITokenClaimsService tokenClaimService)
  {
    _userManager = userManager;
    _tokenClaimService = tokenClaimService;
  }

  private async Task<EfApplicationUser> CreateUser(string username, string password, [EmailAddress] string email)
  {
    Guard.Against.EmailAlreadyTaken(await _userManager.FindByEmailAsync(email));
    Guard.Against.UsernameAlreadyTaken(await _userManager.FindByNameAsync(username));
    var user = new EfApplicationUser {UserName = username, Email = email};
    var result = await _userManager.CreateAsync(user, password);
    
  }


}
