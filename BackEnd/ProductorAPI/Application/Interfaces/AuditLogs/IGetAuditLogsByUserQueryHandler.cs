using Application.DTOs.AuditLogs;
using Application.UseCase.Queries.AuditLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AuditLogs
{
    public interface IGetAuditLogsByUserQueryHandler
    {
        Task<IEnumerable<AuditLogResponse>> Handler(GetAuditLogByUserQuery query);
    }
}
