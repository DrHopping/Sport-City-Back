using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportCity.Core.Interfaces;
using SportCity.SharedKernel;

namespace SportCity.Infrastructure.Identity.Services;

public class IdentityTokenClaimService : ITokenClaimsService
{
    private readonly UserManager<EfApplicationUser> _userManager;
    private readonly AppSettings _appSettings;


    public IdentityTokenClaimService(UserManager<EfApplicationUser> userManager, IOptions<AppSettings> appSettings)
    {
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    public async Task<string> GetTokenAsync(string id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
        var user = await _userManager.FindByIdAsync(id);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
          new(ClaimTypes.Name, user.UserName),
          new(ClaimTypes.NameIdentifier, user.Id),
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GetUserIdentity(string token)
    {
      var decodedToken = DecodeToken(token);
      return decodedToken.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }

    public List<string> GetUserRoles(string token)
    {
      var decodedToken = DecodeToken(token);
      return decodedToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
    }
    
    private JwtSecurityToken DecodeToken(string token)
    {
      var handler = new JwtSecurityTokenHandler();
      return handler.ReadJwtToken(token);
    }
    
    
}
