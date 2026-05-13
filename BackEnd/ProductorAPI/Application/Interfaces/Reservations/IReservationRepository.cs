using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Reservations
{
    public interface IReservationRepository
    {
        Task CreateReservationAsync(Reservation reservation,CancellationToken ct = default);
        Task<Reservation?> GetByIdAsync(Guid id,CancellationToken ct = default);
        Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId,CancellationToken ct = default);
        Task CancelReservation(Reservation reservation,CancellationToken ct = default);

        Task<IEnumerable<Reservation>> GetExpiredReservationsAsync(CancellationToken ct = default);

        Task<Reservation> ConfirmReservationAsync(Reservation reservation, CancellationToken ct = default);
    }
}
