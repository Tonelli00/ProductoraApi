using Application.DTOs.Seat;
using Application.UseCase.Queries.Seats;

namespace Application.Interfaces.Seats
{
    public interface IGetReservedSeatsByEventHandler
    {
        Task<IEnumerable<SeatResponseDTO>> Handle(GetReservedSeatsByEventIdQuery Query);
    }
}
