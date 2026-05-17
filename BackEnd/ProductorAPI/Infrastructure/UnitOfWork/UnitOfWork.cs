using Application.Interfaces;
using Domain.Exceptions.Seats;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(ct);
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            if(_transaction == null)
            {
                throw new InvalidOperationException("No se inicio la transacción");
            }
            try
            {
                 await _context.SaveChangesAsync(ct);
                await _transaction.CommitAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            catch (DbUpdateConcurrencyException)
            {
                await _transaction.RollbackAsync(ct);  
                await _transaction.DisposeAsync();
                _transaction = null;
                throw new SeatConcurrenceException("El asiento ya ha sido reservado por otro usuario.");
            }
        }

        public async Task RollBackAsync(CancellationToken ct = default)
        {
            if(_transaction == null)
            {
                return;
            }
            await _transaction.RollbackAsync(ct);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
