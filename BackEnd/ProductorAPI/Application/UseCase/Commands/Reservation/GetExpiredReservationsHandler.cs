

using Application.DTOs.Reservation;
using Application.Interfaces.Reservations;

namespace Application.UseCase.Commands.Reservation
{
    public class GetExpiredReservationsHandler : IGetExpiredReservationsHandler
    {
        public async Task<IEnumerable<ReservationResponse>> Handler()
        {
            throw new NotImplementedException();
        }
    }
}
