using Application.DTOs;
using Application.DTOs.Event;
using Application.Interfaces.Events;

namespace Application.UseCase.Queries.Events
{
    public class GetPagedEventsHandler : IGetPagedEventsHandler
    {
        private readonly IEventRepository _eventRepository;

        public GetPagedEventsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventShortResponseDTO>> Handle(int Page, int PageSize=10)
        {
            if (Page <= 0)
            {
                throw new ArgumentException("Ingrese una página válida");
            }

            var events = await _eventRepository.GetPagedEvents(Page, PageSize);
            return events.Select(e => new EventShortResponseDTO
            {
                Id = e.Id,
                Name= e.Name,
                EventDate = e.EventDate,
                Venue = e.Venue,   
                Status = e.Status,
                Sectors = e.Sectors.Select(s => new SectorShortResponseDTO()
                {
                    SectorId = s.Id,
                    Name = s.Name,
                    Capacity = s.Capacity,
                    Price = s.Price
                }).ToList()
            }).ToList();
        }
    }
}
