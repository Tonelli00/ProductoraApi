

using Application.DTOs.Event;

namespace Application.Interfaces.Events
{
    public interface IGetPagedEventsQueryHandler
    {
        Task<IEnumerable<EventShortResponseDTO>> Handle(int Page,int PageSize=10);
    }
}
