using Domain.Entities;

namespace Application.Interfaces.Reservations
{
    public interface IGetReservationByIdQueryHandler
    {
        Task<Reservation> Handle(Guid ReservationId);
    }
}
