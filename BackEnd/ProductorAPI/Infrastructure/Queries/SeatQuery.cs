

using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class SeatQuery : ISeatQuery
    {
        private readonly AppDbContext _context;

        public SeatQuery(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Seat>> GetAllSeatsAsync(CancellationToken ct = default)
        {
            return await _context.Seats.ToListAsync(ct);
        }
    }
}
