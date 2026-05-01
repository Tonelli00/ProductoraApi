using Application.DTOs.AuditLogs;
using Application.Interfaces.AuditLogs;
using Application.Interfaces.Users;
using Application.UseCase.Queries.AuditLogs;
using Application.UseCase.Queries.Users;
using Domain.Entities;
using Domain.Exceptions;
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
        private readonly IGetUserByIdHandler _getUserByIdHandler;

        public GetAuditLogByUserHandler(IAuditLogRepository auditLogRepository, IGetUserByIdHandler getUserById)
        {
            _auditLogRepository = auditLogRepository;
            _getUserByIdHandler = getUserById;
        }

        public async Task<IEnumerable<AuditLogResponse>> Handler(GetAuditLogByUserQuery query)
        {
            if(query.UserId<0 || query.UserId == 0) 
            {
                throw new ArgumentException("Ingrese valores válidos");
            }

            var user = await _getUserByIdHandler.Handler(new GetUserByIdQuery { UserId = query.UserId });
            
            if(user == null) 
            {
                throw new UserNotFoundException("El usuario no existe.");
            }

            var auditLogs = await _auditLogRepository.GetAuditLogsByUserId(query.UserId);
            
            if (auditLogs == null) 
                throw new AuditLogNotFoundException("No se encontraron registros de auditoría para el usuario especificado.");
            
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
