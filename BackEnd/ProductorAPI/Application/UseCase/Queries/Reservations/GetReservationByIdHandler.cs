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
    public class GetReservationByIdHandler : IGetReservationByIdQueryHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationByIdHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationResponse> Handle(GetReservationByIdQueryHandler query)
        {
            var reservation = await _reservationRepository.GetByIdAsync(query.reservationId);
            if(reservation == null)
            {
                throw new Exception("Reservation not found");
            }
            return new ReservationResponse
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                SeatId = reservation.SeatId,
                Status = reservation.Status,
                ReservedAt = reservation.ReservedAt,
                ExpiresAt = reservation.ExpiresAt
            };
        }
    }
}
