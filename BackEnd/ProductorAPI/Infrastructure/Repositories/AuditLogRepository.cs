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

        public async Task CreateAuditLog(Audit_Log audit_Log)
        {
            _context.Audit_Logs.Add(audit_Log);
            await _context.SaveChangesAsync();
        }

        public async Task<Audit_Log?> GetAuditLogById(Guid Id)
        {
            return await _context.Audit_Logs.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<IEnumerable<AuditLogResponse>> GetAuditLogsByUserId(int userId)
        {
            return await _context.Audit_Logs
                .Where(a => a.UserId == userId)
                .Select(a => new AuditLogResponse
                {
                    Id = a.Id,
                    Action = a.Action,
                    EntityType = a.EntityType,
                    EntityId = a.EntityId,
                    Details = a.Details,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();
        }

        Task<IEnumerable<Audit_Log>> IAuditLogRepository.GetAuditLogsByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
