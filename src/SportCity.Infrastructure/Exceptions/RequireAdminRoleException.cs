using SportCity.SharedKernel.Exceptions;

namespace SportCity.Infrastructure.Exceptions;

public class RequireAdminRoleException : ForbiddenException
{
  public RequireAdminRoleException() : base($"This operation require admin rights to be performed") { }
}
