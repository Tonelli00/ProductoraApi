
namespace Application.UseCase.Commands.Reservation
{
    public class CreateReservationCommand
    {
        public int UserId { get; set; }
        public Guid SeatId { get; set; }
        public int SeatNumber { get; set; }
        public int SectorId { get; set; }
    }
}
