using SportCity.SharedKernel.Exceptions;

namespace SportCity.Core.Entities.EventAggregate.Exceptions;

public class EventIsFullExceptions : BadRequestException
{
    public EventIsFullExceptions(int capacity) : base($"Event is full and have only {capacity} places") { }
}
