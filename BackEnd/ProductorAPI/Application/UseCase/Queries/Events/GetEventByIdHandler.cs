using Application.DTOs;
using Application.DTOs.Event;
using Application.DTOs.Seat;
using Application.Interfaces.Events;
using Domain.Exceptions;

namespace Application.UseCase.Queries.Events;

public class GetEventByIdHandler:IGetEventByIdHandler
{
    private readonly IEventRepository _repository;

    public GetEventByIdHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<EventResponseDTO> Handle(GetEventByIdQuery query)
    {
        if (query.EventId < 0 || query.EventId == 0 || query.EventId.Equals(null))
        {
            throw new ArgumentException("Ingrese valores correctos");
        }
        
        var _event = await _repository.GetEventById(query.EventId) ?? throw new EventNotFoundException("El evento solicitado no existe..");
        

        return new EventResponseDTO
        {
            Id = _event.Id,
            Name = _event.Name,
            EventDate = _event.EventDate,
            Venue = _event.Venue,
            Status =_event.Status,
            Sectors = _event.Sectors.Select(s=> new SectorResponseDTO
            {
                SectorId = s.Id,
                Name = s.Name,
                Capacity = s.Capacity,
                Price = s.Price,
                Seats = s.Seats.Select(seat => new SeatResponseDTO
                {
                    SeatId = seat.Id,
                    RowIdentifier = seat.RowIdentifier,
                    SeatNumber = seat.SeatNumber,
                    SectorId=s.Id,
                    Status = seat.Status
                }).ToList()
            }).ToList()
        };
    }
}