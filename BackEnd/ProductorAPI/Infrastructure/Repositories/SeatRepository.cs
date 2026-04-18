using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class SeatRepository : ISeatRepository
    {

        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetSeatsBySectorId(int SectorId,CancellationToken ct=default)
        {
            return await _context.Seats.Where(seat => seat.SectorId == SectorId).OrderBy(seat => seat.SeatNumber).ToListAsync(ct);
        }
    }
}
