
using Domain.Entities;

namespace Application.UseCase.Commands.Reservation
{
    public class CancelReservationCommand
    {
        public Guid ReservationId { get; set; }
    }
}
