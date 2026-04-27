

using Domain.Entities;

namespace Application.Interfaces.Events
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetPagedEvents(int Page,int PageSize,CancellationToken ct = default);
        Task<Event> GetEventById(int EventId, CancellationToken ct = default);
        
        
    }
}
