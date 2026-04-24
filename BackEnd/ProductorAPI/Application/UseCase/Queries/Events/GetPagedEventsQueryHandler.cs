using Application.DTOs.Event;
using Application.Interfaces.Events;

namespace Application.UseCase.Queries.Events
{
    public class GetPagedEventsQueryHandler : IGetPagedEventsQueryHandler
    {
        private readonly IEventRepository _eventRepository;

        public GetPagedEventsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventResponse>> Handle(int Page, int PageSize=10)
        {
            var events = await _eventRepository.GetPagedEvents(Page, PageSize);
            return events.Select(e => new EventResponse
            {
                Id = e.Id,
                Name= e.Name,
                EventDate = e.EventDate,
                Venue = e.Venue,   
                Status = e.Status,
            }).ToList();
        }
    }
}
