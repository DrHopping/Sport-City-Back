using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCity.Core.Interfaces;
using SportCity.SharedKernel;
using SportCity.Web.Models;

namespace SportCity.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest request,
        CancellationToken cancellationToken = new())
    {
        var user = await _userService.CreateUser(request.FirstName, request.LastName, request.Password, request.Email);
        return Ok(_mapper.Map<UserCreateResponse>(user));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = new())
    {
        var users = await _userService.GetAllUsers();
        return Ok(_mapper.Map<List<UserResponse>>(users));
    }
}
