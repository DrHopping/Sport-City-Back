﻿using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportCity.Core.Guards;
using SportCity.Core.Interfaces;
using SportCity.Core.User;
using SportCity.Core.User.Events;
using SportCity.Infrastructure.Guards;
using SportCity.SharedKernel;

namespace SportCity.Infrastructure.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<EfApplicationUser> _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserService(UserManager<EfApplicationUser> userManager, IAuthorizationService authorizationService,
        IMediator mediator, IMapper mapper)
    {
        _userManager = userManager;
        _authorizationService = authorizationService;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<User> CreateUser(string firstName, string lastName, string password, [EmailAddress] string email)
    {
        Guard.Against.EmailAlreadyTaken(await _userManager.FindByEmailAsync(email));

        var user = new EfApplicationUser { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);
        Guard.Against.BadPassword(result);

        await AddToRole(user, Roles.User);
        user = await _userManager.FindByEmailAsync(user.Email);
        var userModel = new User { FirstName = firstName, LastName = lastName, IdentityId = user.Id };
        await _mediator.Publish(new NewUserCreatedEvent(userModel));
        return userModel;
    }

    private async Task AddToRole(EfApplicationUser user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task PromoteUser(string id)
    {
        var user = await GetUser(id);
        await _userManager.AddToRoleAsync(user, Roles.Admin);
    }

    public async Task DemoteUser(string id)
    {
        var user = await GetUser(id);
        await _userManager.RemoveFromRoleAsync(user, Roles.Admin);
    }

    public async Task<List<User>> GetAllUsers()
    {
        Guard.Against.NotAdmin(_authorizationService);
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<List<User>>(users);
    }

    private async Task<EfApplicationUser> GetUser(string id)
    {
        Guard.Against.NotAdmin(_authorizationService);
        var user = await _userManager.FindByIdAsync(id);
        Guard.Against.EntityNotFound(user, id, nameof(id));
        return user;
    }
}
