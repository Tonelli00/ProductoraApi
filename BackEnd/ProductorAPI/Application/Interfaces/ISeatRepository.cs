
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetSeatsBySectorId(int SectorId,CancellationToken ct=default);
    }
}
