using SportCity.SharedKernel.Exceptions;

namespace SportCity.Infrastructure.Exceptions;

public class WrongPasswordException : BadRequestException
{
  public WrongPasswordException() : base($"Wrong password") { }
}
