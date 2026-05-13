using Application.DTOs.AuditLogs;
using Application.Interfaces.AuditLogs;
using Domain.Entities;

namespace Application.UseCase.Commands.AuditLog
{
    public class CreateAuditLogHandler :ICreateAuditLogHanlder
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public CreateAuditLogHandler(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogResponse> Handler(CreateAuditLogCommand command)
        {
            if(command == null)
                throw new ArgumentNullException("No hay datos para crear el registro de auditoría");
            if(command.UserId <= 0)
                throw new ArgumentException("El UserId debe ser un número positivo");
            if(string.IsNullOrEmpty(command.Action))
                throw new ArgumentException("La acción no puede ser nula o vacía");
            if(string.IsNullOrEmpty(command.EntityType))
                throw new ArgumentException("El tipo de entidad no puede ser nulo o vacío");
            if(string.IsNullOrEmpty(command.EntityId))
                throw new ArgumentException("El Id de la entidad no puede ser nulo o vacío");
            if(string.IsNullOrEmpty(command.Details))
                throw new ArgumentException("Los detalles no pueden ser nulos o vacíos");

            // crear auditoria
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
