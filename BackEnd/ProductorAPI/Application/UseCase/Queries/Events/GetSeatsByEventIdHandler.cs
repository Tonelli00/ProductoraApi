using Application.DTOs.Seat;
using Application.Interfaces.Events;
using Application.Interfaces.Seats;

namespace Application.UseCase.Queries.Events;

public class GetSeatsByEventIdHandler:IGetSeatsByEventIdHandler
{
    private readonly ISeatRepository _seatRepository;

    public GetSeatsByEventIdHandler(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    public async Task<List<SeatResponseDTO>> Handle(GetSeatsByEventIdQuery query)
    {
        if (query.EventId <= 0)
        {
            throw new ArgumentException("Ingrese un evento válido");
        }

        var result = await _seatRepository.GetSeatsByEventId(query.EventId);
        return result.Select(s => new SeatResponseDTO
        {
            SeatId = s.Id,
            RowIdentifier = s.RowIdentifier,
            SeatNumber = s.SeatNumber,
            Status = s.Status
        }).ToList();
    }
}