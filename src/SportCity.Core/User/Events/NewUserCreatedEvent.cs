using SportCity.SharedKernel;

namespace SportCity.Core.User.Events;

public class NewUserCreatedEvent : BaseDomainEvent
{
    public User NewUser { get; set; }

    public NewUserCreatedEvent(User newUser)
    {
        NewUser = newUser;
    }
}
