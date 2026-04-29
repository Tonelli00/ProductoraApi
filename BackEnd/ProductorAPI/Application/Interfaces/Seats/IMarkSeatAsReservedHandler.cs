

using Application.UseCase.Commands.Seat;

namespace Application.Interfaces.Seats
{
    public interface IMarkSeatAsReservedHandler
    {
        Task Handle(MarkSeatAsReservedCommand Command);
    }
}
