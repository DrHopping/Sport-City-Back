using Ardalis.GuardClauses;
using MediatR;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.User.Events;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.User.Handlers;

public class NewUserCreatedHandler : INotificationHandler<NewUserCreatedEvent>
{
    private readonly IRepository<Player> _repository;

    public NewUserCreatedHandler(IRepository<Player> repository)
    {
        _repository = repository;
    }

    public async Task Handle(NewUserCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        Guard.Against.Null(domainEvent, nameof(domainEvent));
        var user = domainEvent.NewUser;
        var player = new Player(user.IdentityId, user.FirstName, user.LastName, 1);
        await _repository.AddAsync(player, cancellationToken);
    }
}
