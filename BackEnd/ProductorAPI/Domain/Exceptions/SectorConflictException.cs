namespace Domain.Exceptions;

public class SectorConflictException : Exception
{
    public SectorConflictException(string message) : base(message) { }
}