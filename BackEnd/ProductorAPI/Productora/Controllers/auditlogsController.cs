using Application.Interfaces.AuditLogs;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Queries.AuditLogs;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly IGetAuditLogsByUserQueryHandler _getAuditLogByUserQueryHandler;

        public AuditLogsController(IGetAuditLogsByUserQueryHandler getAuditLogByUserQueryHandler)
        {
            _getAuditLogByUserQueryHandler = getAuditLogByUserQueryHandler;
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var result = await _getAuditLogByUserQueryHandler.Handler(new GetAuditLogByUserQuery { UserId = userId});
            return Ok(result);
        }
    }
}
