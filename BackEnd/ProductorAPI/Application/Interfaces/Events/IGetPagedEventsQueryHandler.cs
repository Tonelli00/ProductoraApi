

using Application.DTOs.Event;

namespace Application.Interfaces.Events
{
    public interface IGetPagedEventsQueryHandler
    {
        Task<IEnumerable<EventResponse>> Handle(int Page,int PageSize=10);
    }
}
