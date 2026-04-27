using Application.DTOs.Event;
using Application.UseCase.Queries.Events;

namespace Application.Interfaces.Events;

public interface IGetEventByIdHandler
{
    Task<EventResponseDTO> Handle(GetEventByIdQuery query);
}