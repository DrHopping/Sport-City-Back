using Microsoft.Net.Http.Headers;
using SportCity.Core.Interfaces;

namespace SportCity.Web.Middleware;

public class TokenHandlerMiddleware : IMiddleware
{
    private readonly IAuthorizationService _authorizationService;

    public TokenHandlerMiddleware(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Headers.TryGetValue(HeaderNames.Authorization, out var token))
        {
            _authorizationService.SetToken(token.ToString().Split(' ')[1]);
        }

        await next(context);
    }
}
