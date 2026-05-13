
using Application.DTOs.Reservation;
using Application.UseCase.Commands.Reservation;
using Domain.Entities;

namespace Application.Interfaces.Reservations
{
    public interface IConfirmReservationHandler
    {
        Task<ReservationResponse> Handle(ConfirmReservationCommand command);
    }
}
