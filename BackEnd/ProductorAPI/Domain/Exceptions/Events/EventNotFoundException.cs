namespace Domain.Exceptions;

public class EventNotFoundException:Exception
{
    public EventNotFoundException(string message) : base(message) { }
}