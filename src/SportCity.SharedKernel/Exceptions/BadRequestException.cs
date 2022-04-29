using System.Net;

namespace SportCity.SharedKernel.Exceptions;

public class BadRequestException : BaseException
{
  public BadRequestException(params string[] errors) : base(HttpStatusCode.BadRequest, errors) { }
}
