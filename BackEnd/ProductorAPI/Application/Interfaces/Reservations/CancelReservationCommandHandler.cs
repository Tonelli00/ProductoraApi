using Application.DTOs.Reservation;


namespace Application.Interfaces.Reservations
{
    public interface CancelReservationCommandHandler
    {
        Task<ReservationResponse> Handler(Guid ReservationId);
    }
}
