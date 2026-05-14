using Application.DTOs.Seat;
using Application.UseCase.Commands.Seat;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Seats
{
    public interface IMarkSeatAtAvailableHandler
    {
        Task Handle(MarkSeatAsAvailableCommand command);
    }
}
