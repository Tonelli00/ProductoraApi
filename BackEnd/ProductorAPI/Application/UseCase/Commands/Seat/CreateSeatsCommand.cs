namespace Application.UseCase.Commands.Seat;

public class CreateSeatsCommand
{
  
    public int SectorId { get; set; }
    public int SeatsToCreate { get; set; }
}