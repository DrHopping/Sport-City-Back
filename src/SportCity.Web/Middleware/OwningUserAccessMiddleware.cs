using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Interfaces;
using SportCity.SharedKernel.Exceptions;
using SportCity.SharedKernel.Interfaces;
using SportCity.Web.Attributes;
using SportCity.Web.Helpers;

namespace SportCity.Web.Middleware;

public class OwningUserAccessMiddleware : IMiddleware
{
  private readonly IAuthorizationService _authorizationService;

  public OwningUserAccessMiddleware(IAuthorizationService authorizationService)
  {
    _authorizationService = authorizationService;
  }

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    var endpoint = context.GetEndpoint();
    var attr = endpoint?.Metadata.GetMetadata<OwningUserAccess>();
    if (attr != null)
    {
      var methodData = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
      var idParameter = methodData?.Parameters
        .Select(p => (ControllerParameterDescriptor)p)
        .First(p => p.ParameterInfo.GetCustomAttribute<OwningUserAccessId>() != null);
      
      if (idParameter?.Name != null)
      {
        context.Request.RouteValues.TryGetValue(idParameter.Name, out var id);
        var service = context.RequestServices.GetDomainServiceFor(attr.Type);
        var ownerId = await service.GetOwnerId((Convert.ToInt32(id)));
        var grantAccess = _authorizationService.GetIdentity() == ownerId || _authorizationService.IsAdmin();
        if (!grantAccess) throw new ForbiddenException("You must be owner or admin to perform this operation");
      }
    }

    await next(context);
  }
}
