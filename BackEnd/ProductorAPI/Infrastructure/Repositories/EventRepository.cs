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
        public async Task<IEnumerable<Event>> GetAllEvents(CancellationToken ct = default)
        {
            return await _context.Events.OrderByDescending(e => e.Id).ToListAsync(ct);
        }

        public async Task<IEnumerable<Event>> GetPagedEvents(int Page, int PageSize, CancellationToken ct = default)
        {
            return await _context.Events.AsNoTracking().OrderByDescending(e=>e.Id).Skip((Page-1)*PageSize).Take(PageSize).ToListAsync(ct);
        }
    }
}
