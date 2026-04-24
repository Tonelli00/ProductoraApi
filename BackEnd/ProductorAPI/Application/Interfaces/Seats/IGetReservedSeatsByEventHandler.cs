using Application.DTOs.Seat;
using Application.UseCase.Queries.Seats;

namespace Application.Interfaces.Seats
{
    public interface IGetReservedSeatsByEventHandler
    {
        Task<IEnumerable<SeatResponse>> Handle(GetReservedSeatsByEventIdQuery Query);
    }
}
