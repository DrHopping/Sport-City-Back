namespace SportCity.Core.Entities.EventAggregate.Exceptions;

public class EventIsFullExceptions : Exception
{
    public EventIsFullExceptions(int capacity) : base($"Event is full and have only {capacity} places") { }
}
