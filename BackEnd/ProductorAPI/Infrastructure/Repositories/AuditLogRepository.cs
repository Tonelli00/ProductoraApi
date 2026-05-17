using Application.DTOs.AuditLogs;
using Application.Interfaces.AuditLogs;
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
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;
        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAuditLog(Audit_Log audit_Log, CancellationToken ct = default)
        {
           await _context.Audit_Logs.AddAsync(audit_Log, ct);
           await _context.SaveChangesAsync(ct);

        }

        public async Task<Audit_Log?> GetAuditLogById(Guid Id, CancellationToken ct = default)
        {
            return await _context.Audit_Logs.FirstOrDefaultAsync(a => a.Id == Id,ct);
        }

        public async Task<IEnumerable<Audit_Log>> GetAuditLogsByUserId(int userId, CancellationToken ct = default)
        {
            return await _context.Audit_Logs.Where(a => a.UserId == userId).ToListAsync(ct);
        }

        
    }
}
