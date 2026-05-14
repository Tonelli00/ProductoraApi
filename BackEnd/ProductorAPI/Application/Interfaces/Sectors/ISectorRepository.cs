using Domain.Entities;

namespace Application.Interfaces.Sectors;

public interface ISectorRepository
{
    Task<Sector> InsertAsync(Sector Sector, CancellationToken ct = default);
    Task<Sector> GetSectorByIdAsync(int Id,CancellationToken ct = default);
    Task<Sector> GetSectorSummaryByIdAsync(int Id, CancellationToken ct = default);
}