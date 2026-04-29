using Application.DTOs.Reservation;
using Application.Interfaces.Reservations;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Queries.Reservations
{
    public class GetReservationsByUserHandler : IGetReservationsByUserQueryHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationsByUserHandler(IReservationRepository reservationRepository)
        {
            this._reservationRepository = reservationRepository;
        }
      
        public async Task<IEnumerable<ReservationResponse>> Handler(GetReservationsByUserIDQuery query)
        {
            var reservations = await _reservationRepository.GetByUserIdAsync(query.userId);
            return reservations.Select(r => new ReservationResponse
            {
                Id = r.Id,
                UserId = r.UserId,
                SeatId = r.SeatId,
                Status = r.Status,
                ReservedAt = r.ReservedAt,
                ExpiresAt = r.ExpiresAt
            }).ToList();
        }
    }
}
