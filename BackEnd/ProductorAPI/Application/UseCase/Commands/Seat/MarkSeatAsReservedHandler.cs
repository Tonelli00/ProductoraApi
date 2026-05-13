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
            command.Seat.Status="Reserved";
            command.Seat.Version += 1;
            await _repository.UpdateSeatStatus(command.Seat);               
        }
    }
}
