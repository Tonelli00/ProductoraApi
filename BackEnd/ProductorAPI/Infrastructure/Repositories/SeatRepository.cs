using Application.Interfaces.Seats;
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

        public async Task InsertSeatsAsync(List<Seat> Seats, CancellationToken ct = default)
        {
            await _context.Seats.AddRangeAsync(Seats, ct);
            await _context.SaveChangesAsync(ct);
        }
        
        public async Task<Seat> GetSeatById(Guid SeatId, CancellationToken ct = default)
        {
            return await _context.Seats.Include(s=>s.Sector).FirstOrDefaultAsync(s => s.Id == SeatId, ct);
        }
        
        public async Task UpdateSeatStatus(Seat seat, CancellationToken ct = default)
        {
            _context.Seats.Update(seat);
            await _context.SaveChangesAsync(ct);
        }
        
        public async Task<IEnumerable<Seat>> GetReservedSeatsByEventId(int EventId,CancellationToken ct = default)
        {
            return await _context.Seats.Where(s => s.Sector.EventId == EventId && s.Status == "Reserved").ToListAsync(ct);
        }
        


        public async Task<Seat> GetSeatBySeatNumberAndSectorId(int SeatNumber, int SectorId, CancellationToken ct = default)
        {
            return await _context.Seats.FirstOrDefaultAsync(s => s.SeatNumber == SeatNumber && s.SectorId==SectorId, ct);
        }

        

        public async Task<IEnumerable<Seat>> GetSeatsBySectorId(int SectorId,CancellationToken ct=default)
        {
            return await _context.Seats.Where(seat => seat.SectorId == SectorId).OrderBy(seat => seat.SeatNumber).ToListAsync(ct);
        }

    
        
        public async Task<IEnumerable<Seat>> GetSeatsByEventId(int EventId, CancellationToken ct = default)
        {
            return await _context.Seats.Include(s=>s.Sector)
                .Where(s => s.SectorId != null && s.Sector.EventId == EventId).OrderBy(s=>s.SeatNumber)
                .ToListAsync(ct);
        }
        
    }
}
