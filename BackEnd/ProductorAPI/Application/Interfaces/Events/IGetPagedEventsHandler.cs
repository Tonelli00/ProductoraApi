

using Application.DTOs.Event;

namespace Application.Interfaces.Events
{
    public interface IGetPagedEventsHandler
    {
        Task<IEnumerable<EventShortResponseDTO>> Handle(int Page,int PageSize=10);
    }
}
