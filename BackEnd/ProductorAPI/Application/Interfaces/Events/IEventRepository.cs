

using Domain.Entities;

namespace Application.Interfaces.Events
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEvents(CancellationToken ct = default);
        Task<IEnumerable<Event>> GetPagedEvents(int Page,int PageSize,CancellationToken ct = default);
    }
}
