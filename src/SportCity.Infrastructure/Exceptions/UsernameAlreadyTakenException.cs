using SportCity.SharedKernel.Exceptions;

namespace SportCity.Infrastructure.Exceptions;

public class UsernameAlreadyTakenException : BadRequestException
{
    public UsernameAlreadyTakenException(string username) : base($"Username {username} is already taken") { }
}
