using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Interfaces;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [AllowAnonymous]
  public class AuthController : ControllerBase
  {
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
    {
      var result = await _authenticationService.Authenticate(request.Email, request.Password);
      return Ok(result);
    }
    
    [HttpPost]
    [Route("admin")]
    public async Task<IActionResult> AuthenticateAdminTest()
    {
      var result = await _authenticationService.Authenticate("admin@microsoft.com", "Admin123!");
      return Ok(result);
    }
    
  }
}
