using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Interfaces;
using SportCity.Core.Services;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public AuthController(IAuthenticationService authenticationService, IPlayerService playerService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _playerService = playerService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
        {
            var user = await _authenticationService.Authenticate(request.Email, request.Password);
            var player = await _playerService.GetPlayerById(user.Id);
            return Ok(new
            {
                IdentityId = user.Id,
                Player = _mapper.Map<PlayerListResponse>(player),
                Token = user.Token,
                Role = user.Role
            });
        }

        [HttpGet]
        public async Task<IActionResult> EmailExists([FromQuery] string email)
        {
            return Ok(await _authenticationService.EmailExists(email));
        }

        [HttpPost]
        [Route("admin")]
        public async Task<IActionResult> AuthenticateAdminTest()
        {
            var user = await _authenticationService.Authenticate("admin@microsoft.com", "Admin123!");
            var player = await _playerService.GetPlayerById(user.Id);
            return Ok(new
            {
                IdentityId = user.Id,
                Player = player,
                Token = user.Token,
                Role = user.Role
            });
        }
    }
}
