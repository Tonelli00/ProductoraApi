
using Application.DTOs.Event;
using Application.Interfaces.Events;

namespace Application.UseCase.Handlers.Events
{
    public class GetAllEventsQueryHandler : IGetAllEventsQueryHandler
    {
        private readonly IEventRepository _eventRepository;

        public GetAllEventsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventResponse>> Handle()
        {
            var events = await _eventRepository.GetAllEvents();
            return events.Select(e => new EventResponse
            {
                Name = e.Name,
                EventDate = e.EventDate,
                Venue = e.Venue,
                Status = e.Status,
            }).ToList();
        }
    }
}
