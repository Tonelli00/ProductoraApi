
using Application.UseCase.Commands.Seat;

namespace Application.Interfaces.Seats
{
    public interface IMarkSeatAsSoldHandler
    {
        public Task Handle(MarkSeatAsSoldCommand command);
    }
}
