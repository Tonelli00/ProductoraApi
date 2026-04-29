namespace Domain.Exceptions;

public class SectorNotFoundException:Exception
{
    public SectorNotFoundException(string message) : base(message) { }
}