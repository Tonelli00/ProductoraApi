using Application.DTOs.AuditLogs;
using Application.Interfaces.AuditLogs;
using Domain.Entities;

namespace Application.UseCase.Commands.AuditLog
{
    public class CreateAuditLogHandler :ICreateAuditLogCommandHanlder
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public CreateAuditLogHandler(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogResponse> Handler(CreateAuditLogCommand command)
        {
            var auditLog = new Audit_Log
            {
                UserId = command.UserId,
                Action = command.Action,
                EntityType = command.EntityType,
                EntityId = command.EntityId,
                Details = command.Details,
                CreatedAt = DateTime.UtcNow
            };

            // guardar auditoria
            await _auditLogRepository.CreateAuditLog(auditLog);

            // devovler respuesta
            return new AuditLogResponse
            {
                Id = auditLog.Id,
                UserId = auditLog.UserId,
                Action = auditLog.Action,
                EntityType = auditLog.EntityType,
                EntityId = auditLog.EntityId,
                Details = auditLog.Details,
                CreatedAt = auditLog.CreatedAt
            };
        }
    }
}
