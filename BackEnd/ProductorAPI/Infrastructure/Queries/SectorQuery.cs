

using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class SectorQuery : ISectorQuery
    {
        private readonly AppDbContext _context;

        public SectorQuery(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetAllSeatsBySectorId(int SectorId, CancellationToken ct = default)
        {
            return await _context.Seats.Where(seat => seat.SectorId == SectorId).OrderBy(s=>s.SeatNumber).ToListAsync(ct);
        }
    }
}
