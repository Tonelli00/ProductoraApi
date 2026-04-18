using Application.DTOs.Event;
using System;


namespace Application.Interfaces.Events
{
    public interface IGetAllEventsQueryHandler
    {
        Task<IEnumerable<EventResponse>> Handle();
    }
}
