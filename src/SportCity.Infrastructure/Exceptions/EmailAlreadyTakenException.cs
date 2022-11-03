using SportCity.SharedKernel.Exceptions;

namespace SportCity.Infrastructure.Exceptions;

public class EmailAlreadyTakenException : BadRequestException
{
    public EmailAlreadyTakenException(string email) : base($"Email {email} is already taken") { }
}
