using Application.DTOs.Seat;
using Application.UseCase.Queries.Events;

namespace Application.Interfaces.Events;

public interface IGetSeatsByEventIdHandler
{
    Task<List<SeatResponseDTO>> Handle(GetSeatsByEventIdQuery query);
}