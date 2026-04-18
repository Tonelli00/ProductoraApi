

using Application.UseCase.Commands.Seat;

namespace Application.Interfaces.Seats
{
    public interface IMarkSeatAsReservedCommandHandler
    {
        Task Handle(MarkSeatAsReservedCommand Command);
    }
}
