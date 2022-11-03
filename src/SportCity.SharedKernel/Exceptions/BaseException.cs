using System.Net;
using System.Text.Json;

namespace SportCity.SharedKernel.Exceptions;

public class BaseException : Exception
{
    public string[] Errors { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public BaseException(HttpStatusCode statusCode, params string[] errors)
    {
        Errors = errors;
        StatusCode = statusCode;
    }
}
