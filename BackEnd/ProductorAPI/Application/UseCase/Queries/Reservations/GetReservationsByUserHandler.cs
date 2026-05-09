using Application.DTOs.Reservation;
using Application.Interfaces.Reservations;
using Application.Interfaces.Users;
using Application.UseCase.Queries.Users;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Queries.Reservations
{
    public class GetReservationsByUserHandler : IGetReservationsByUserQueryHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGetUserByIdHandler _getUserByIdQuery;



        public GetReservationsByUserHandler(IReservationRepository reservationRepository, IGetUserByIdHandler getUserByIdQuery)
        {
            this._reservationRepository = reservationRepository;
            this._getUserByIdQuery = getUserByIdQuery;
        }

        public async Task<IEnumerable<ReservationResponse>> Handler(GetReservationsByUserIDQuery query)
        {
            if(query.userId<0 || query.userId == 0) 
            {
                throw new ArgumentException("Ingrese valores válidos");
            }

            var user = await _getUserByIdQuery.Handler(new GetUserByIdQuery { UserId = query.userId });
            if(user == null) 
            {
                throw new UserNotFoundException("El usuario solicitado no existe");
            }
            var reservations = await _reservationRepository.GetByUserIdAsync(query.userId);
            if (reservations == null)
                throw new ReservationNotFoundException("No se encontraron reservasiones para el usuario especificado.");
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
