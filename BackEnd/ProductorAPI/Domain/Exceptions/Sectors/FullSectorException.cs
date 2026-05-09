namespace Domain.Exceptions;

public class FullSectorException:Exception
{
    public FullSectorException(string message) : base(message) { }
}