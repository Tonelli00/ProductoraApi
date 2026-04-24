using Application.DTOs.Reservation;
using Application.UseCase.Commands.Reservation;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Reservations
{
    public interface ICreateReservationCommandHandler
    {
        Task<ReservationResponse> Handle(CreateReservationCommand command);
    }
}
