

namespace Application.UseCase.Commands.Seat
{
    public class MarkSeatAsReservedCommand
    {
        public int SeatNumber { get; set; }
        public int SectorId { get; set; }
        //Podría recibir un user Id tambien ? 
    }
}
