namespace Domain.Exceptions;

public class ReservedSeatException:Exception
{
    public ReservedSeatException(string message) : base(message) { }
}