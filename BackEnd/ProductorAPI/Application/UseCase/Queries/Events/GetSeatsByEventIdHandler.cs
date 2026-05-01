using Application.DTOs.Seat;
using Application.Interfaces.Events;
using Application.Interfaces.Seats;
using Domain.Exceptions;

namespace Application.UseCase.Queries.Events;

public class GetSeatsByEventIdHandler : IGetSeatsByEventIdHandler
{
    private readonly IEventRepository _eventRepository;
    private readonly ISeatRepository _seatRepository;

    public GetSeatsByEventIdHandler(ISeatRepository seatRepository, IEventRepository eventRepository)
    {
        _seatRepository = seatRepository;
        _eventRepository = eventRepository;
    }

    public async Task<List<SeatResponseDTO>> Handle(GetSeatsByEventIdQuery query)
    {
        if (query.EventId <= 0)
        {
            throw new ArgumentException("Ingrese un evento válido");
        }

        var _event = await _eventRepository.GetEventById(query.EventId);
        if (_event == null) 
        {
            throw new EventNotFoundException("El evento solicitado no existe");
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