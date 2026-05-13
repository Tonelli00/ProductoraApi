using Application.DTOs.Seat;
using Application.Interfaces.Seats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Commands.Seat
{
    public class MarkSeatAtAvailableHandler : IMarkSeatAtAvailableHandler
    {
        private readonly ISeatRepository _seatRepository;

        public MarkSeatAtAvailableHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task Handle(MarkSeatAtAvailableCommand command)
        {
            command.Seat.Status = "Available";
            await _seatRepository.UpdateSeatStatus(command.Seat);
        }
    }
}
