using Application.DTOs.Reservation;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Reservations
{
    public interface IGetExpiredReservationsHandler
    {
        Task<IEnumerable<ReservationResponse>> Handler();
    }
}
