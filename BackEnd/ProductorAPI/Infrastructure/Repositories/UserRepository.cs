using Application.Interfaces.Users;
using Application.UseCase.Commands.User;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.Users.Add(user);
            await _context.SaveChangesAsync(ct);
        }
    }
}
