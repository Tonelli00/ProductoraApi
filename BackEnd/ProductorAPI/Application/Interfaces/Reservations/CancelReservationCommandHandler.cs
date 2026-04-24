using Application.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Reservations
{
    public interface CancelReservationCommandHandler
    {
        Task<ReservationResponse> Handler(Guid ReservationId);
    }
}
