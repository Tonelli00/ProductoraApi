using Application.Interfaces.Events;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetPagedEvents(int Page, int PageSize, CancellationToken ct = default)
        {
            return await _context.Events.Include(e=> e.Sectors).AsNoTracking().OrderByDescending(e=>e.Id).Skip((Page-1)*PageSize).Take(PageSize).ToListAsync(ct);
        }

        public async Task<Event> GetEventById(int EventId, CancellationToken ct = default)
        {
            return await _context.Events.Include(e => e.Sectors).ThenInclude(s => s.Seats)
                .FirstOrDefaultAsync(e => e.Id == EventId, ct);
        }
    }
}
