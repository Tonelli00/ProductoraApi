using Application.DTOs.Reservation;
using Application.Interfaces.Reservations;

namespace Application.UseCase.Queries.Reservations
{
    public class GetExpiredReservationsHandler : IGetExpiredReservationsHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public GetExpiredReservationsHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<IEnumerable<ReservationResponse>> Handler()
        {
            var reservations = await _reservationRepository.GetExpiredReservationsAsync();
            return reservations.Select(reservations => new ReservationResponse
            {
                Id = reservations.Id,
                UserId = reservations.UserId,
                SeatId = reservations.SeatId,
                Status = reservations.Status.ToString(),
                ReservedAt = reservations.ReservedAt,
                ExpiresAt = reservations.ExpiresAt
            });
        }
    }
}
