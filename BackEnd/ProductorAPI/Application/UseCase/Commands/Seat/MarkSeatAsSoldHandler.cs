

using Application.Interfaces.Seats;

namespace Application.UseCase.Commands.Seat
{
    public class MarkSeatAsSoldHandler : IMarkSeatAsSoldHandler
    {
        private readonly ISeatRepository _seatRepository;

        public MarkSeatAsSoldHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task Handle(MarkSeatAsSoldCommand command)
        {
            var seat = await _seatRepository.GetSeatById(command.SeatId);
            seat.Status = "Sold";
            await _seatRepository.UpdateSeatStatus(seat);
        }
    }
}
