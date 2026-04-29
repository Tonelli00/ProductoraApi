using Application.DTOs.Seat;
using Application.UseCase.Queries.Seats;

namespace Application.Interfaces.Seats
{
    public interface IGetSeatsBySectorIdHandler
    {
        Task<IEnumerable<SeatResponseDTO>> Handle(GetSeatsBySectorIdQuery query);
    }
}
