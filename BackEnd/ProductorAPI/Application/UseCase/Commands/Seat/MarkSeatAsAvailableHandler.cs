using Application.DTOs.Seat;
using Application.Interfaces.Seats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Commands.Seat
{
    public class MarkSeatAsAvailableHandler : IMarkSeatAtAvailableHandler
    {
        private readonly ISeatRepository _seatRepository;

        public MarkSeatAsAvailableHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task Handle(MarkSeatAsAvailableCommand command)
        {
            var seat = await _seatRepository.GetSeatById(command.SeatId);
            seat.Status = "Available";
            await _seatRepository.UpdateSeatStatus(seat);
        }
    }
}
