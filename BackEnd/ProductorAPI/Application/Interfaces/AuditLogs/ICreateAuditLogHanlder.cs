using Application.DTOs.AuditLogs;
using Application.UseCase.Commands.AuditLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AuditLogs
{
    public interface ICreateAuditLogHanlder
    {
        Task<AuditLogResponse> Handler(CreateAuditLogCommand command);
    }
}
