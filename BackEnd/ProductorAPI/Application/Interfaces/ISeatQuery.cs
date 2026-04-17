
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISeatQuery
    {
        Task<IEnumerable<Seat>> GetAllSeatsAsync(CancellationToken ct=default);
    }
}
