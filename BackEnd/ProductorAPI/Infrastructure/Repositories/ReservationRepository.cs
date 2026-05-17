using Application.Interfaces.Reservations;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CancelReservation(Reservation reservation,CancellationToken ct = default)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Reservation> ConfirmReservationAsync(Reservation reservation, CancellationToken ct = default)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(ct);
            return reservation;
        }

        public async Task CreateReservationAsync(Reservation reservation,CancellationToken ct = default)
        {
            await _context.Reservations.AddAsync(reservation, ct);
        }

        public async Task<Reservation?> GetByIdAsync(Guid id,CancellationToken ct = default)
        {
            return await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id,ct);
        }

        public async Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId,CancellationToken ct = default)
        {

            return await _context.Reservations
                .Where(r => r.UserId == userId && (r.Status == "Paid" || r.Status == "Pending"))
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Reservation>> GetExpiredReservationsAsync(CancellationToken ct = default)
        {
            var argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Argentina/Buenos_Aires");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, argentinaTimeZone);
            return await _context.Reservations.Where(reservation => reservation.Status == "Pending" && reservation.ExpiresAt < now).ToListAsync(ct);
        }
    }
}
