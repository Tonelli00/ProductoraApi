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
        Task CreateReservationAsync(Reservation reservation);
        Task<Reservation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId);
        Task CancelReservation(Reservation reservation);
    }
}
