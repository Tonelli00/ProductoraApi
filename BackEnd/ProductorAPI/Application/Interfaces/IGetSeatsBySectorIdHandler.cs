

using Application.DTOs.Seat;
using Application.UseCase.Queries;

namespace Application.Interfaces
{
    public interface IGetSeatsBySectorIdHandler
    {
        Task<IEnumerable<SeatResponse>> Handle(GetSeatsBySectorIdQuery query);
    }
}
