using System.Net;

namespace SportCity.SharedKernel.Exceptions;

public class NotFoundException : BaseException
{
  public NotFoundException(params string[] errors) : base(HttpStatusCode.NotFound, errors) { }
}
