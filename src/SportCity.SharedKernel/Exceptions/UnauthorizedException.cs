using System.Net;

namespace SportCity.SharedKernel.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(params string[] errors) : base(HttpStatusCode.Unauthorized, errors) { }
}
