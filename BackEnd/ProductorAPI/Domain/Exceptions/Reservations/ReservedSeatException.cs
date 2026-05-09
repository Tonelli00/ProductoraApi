namespace Domain.Exceptions.Reservations;

public class ReservedSeatException:Exception
{
    public ReservedSeatException(string message) : base(message) { }
}