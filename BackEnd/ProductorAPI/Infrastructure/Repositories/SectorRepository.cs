using Application.Interfaces.Sectors;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SectorRepository:ISectorRepository
{
    private readonly AppDbContext _context;

    public SectorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Sector> InsertAsync(Sector Sector, CancellationToken ct = default)
    {
        await _context.Sectors.AddAsync(Sector, ct);
        await _context.SaveChangesAsync(ct);
        return Sector;
    }


    public async Task<Sector> GetSectorByIdAsync(int Id, CancellationToken ct = default)
    {
        return await _context.Sectors.Include(s=>s.Seats).FirstOrDefaultAsync(s=> s.Id==Id,ct);
    }
}