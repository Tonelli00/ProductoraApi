using Application.DTOs.AuditLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AuditLogs
{
    public interface GetAuditLogByIdHandler
    {
        Task<AuditLogResponse> Handler(Guid Id);
    }
}
