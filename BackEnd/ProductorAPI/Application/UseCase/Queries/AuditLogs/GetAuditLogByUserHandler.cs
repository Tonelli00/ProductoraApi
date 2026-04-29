using Application.DTOs.AuditLogs;
using Application.Interfaces.AuditLogs;
using Application.UseCase.Queries.AuditLogs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Handlers.AuditLogs
{
    public class GetAuditLogByUserHandler : IGetAuditLogsByUserQueryHandler
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public GetAuditLogByUserHandler(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<IEnumerable<AuditLogResponse>> Handler(GetAuditLogByUserQuery query)
        {
            var auditLogs = await _auditLogRepository.GetAuditLogsByUserId(query.UserId);
            return auditLogs.Select(a => new AuditLogResponse
            {
                Id = a.Id,
                UserId = a.UserId,
                Action = a.Action,
                EntityType = a.EntityType,
                EntityId = a.EntityId,
                Details = a.Details,
                CreatedAt = a.CreatedAt
            }).ToList();
        }
    }
}
