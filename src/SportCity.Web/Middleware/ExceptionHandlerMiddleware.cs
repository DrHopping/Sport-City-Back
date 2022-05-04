using Newtonsoft.Json;
using SportCity.SharedKernel.Exceptions;
using SportCity.Web.Models;

namespace SportCity.Web.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
  private readonly ILogger _logger;
  
  public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
  {
    _logger = logger;
  }
  
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      var message = CreateMessage(context, ex);
      _logger.LogError(message, ex);

      await HandleExceptionAsync(context, ex);
    }
  }
  
  private async Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    var exceptionResponse = new ExceptionResponse();
    int statusCode;
    
    if (exception is BaseException e)
    {
      exceptionResponse.Errors = e.Errors;
      statusCode = (int)e.StatusCode;
    }
    else
    {
      exceptionResponse.Errors = new [] {"Unknown error, please contact the system admin"};
      statusCode = StatusCodes.Status500InternalServerError;
    }
    
    context.Response.StatusCode = statusCode;
    await context.Response.WriteAsJsonAsync(exceptionResponse);
  }
  
  private string CreateMessage(HttpContext context, Exception e)
  {
    var message = $"Exception caught in global error handler, exception message: {e.Message}, exception stack: {e.StackTrace}";

    if (e.InnerException != null)
    {
      message = $"{message}, inner exception message {e.InnerException.Message}, inner exception stack {e.InnerException.StackTrace}";
    }

    return $"{message} RequestId: {context.TraceIdentifier}";
  }
}
