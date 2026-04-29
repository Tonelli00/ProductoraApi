namespace Application.UseCase.Commands.Sector;

public class CreateSectorCommand
{
    public int EventId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Capacity { get; set; }
}