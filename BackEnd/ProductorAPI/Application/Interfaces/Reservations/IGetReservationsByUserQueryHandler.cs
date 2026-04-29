using Application.DTOs.Reservation;
using Application.UseCase.Queries.Reservations;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Reservations
{
    public interface IGetReservationsByUserQueryHandler
    {
        Task<IEnumerable<ReservationResponse>> Handler(GetReservationsByUserIDQuery query);
    }
}
