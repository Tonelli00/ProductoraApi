

using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISectorQuery
    {
        Task<IEnumerable<Seat>> GetAllSeatsBySectorId(int SectorId, CancellationToken ct = default);
    }
}
