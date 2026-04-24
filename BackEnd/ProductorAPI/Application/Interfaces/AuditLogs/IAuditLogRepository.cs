using Application.DTOs.AuditLogs;
using Application.UseCase.Commands.AuditLog;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AuditLogs
{
    public interface IAuditLogRepository
    {
        Task CreateAuditLog(Audit_Log audit_Log);
        Task<IEnumerable<Audit_Log>> GetAuditLogsByUserId(int userId);
        Task<Audit_Log?> GetAuditLogById(Guid Id);
    }
}
