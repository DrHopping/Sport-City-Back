using System.Net;

namespace SportCity.SharedKernel.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException(params string[] errors) : base(HttpStatusCode.Forbidden, errors) { }
}
