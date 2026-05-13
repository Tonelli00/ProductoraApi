using Application.Interfaces.Users;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(int id, CancellationToken ct = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id,ct);
        }

        public async Task InsertAsync(User user, CancellationToken ct = default)
        {
            _context.Users.AddAsync(user, ct);
            await _context.SaveChangesAsync();
        }


        public async Task<User?> GetUserByEmailAsync(string userEmail, CancellationToken ct = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.Email==userEmail,ct);
        }

    }
}
