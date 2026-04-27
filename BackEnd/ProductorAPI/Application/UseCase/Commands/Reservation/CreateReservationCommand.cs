
namespace Application.UseCase.Commands.Reservation
{
    public class CreateReservationCommand
    {
        public int UserId { get; set; }
       
        public Guid SeatId { get; set; }
        
    }
}
