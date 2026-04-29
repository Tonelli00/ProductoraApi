using Application.DTOs.Reservation;
using Application.UseCase.Queries.Reservations;
using Domain.Entities;

namespace Application.Interfaces.Reservations
{
    public interface IGetReservationByIdQueryHandler
    {
        Task<ReservationResponse> Handle(GetReservationByIdQueryHandler query);
    }
}
