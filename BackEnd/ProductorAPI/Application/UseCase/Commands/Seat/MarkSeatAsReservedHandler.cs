using Application.Interfaces.Seats;
using Domain.Exceptions;
using Domain.Entities;

namespace Application.UseCase.Commands.Seat
{
    public class MarkSeatAsReservedHandler : IMarkSeatAsReservedHandler
    {
        private readonly ISeatRepository _repository;
        

        public MarkSeatAsReservedHandler(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(MarkSeatAsReservedCommand command)
        {
            var seat = await _repository.GetSeatById(command.SeatId);
            seat.Status = "Reserved";
            seat.Version += 1;
            await _repository.UpdateSeatStatus(seat);               
        }
    }
}
