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
        private readonly ICreateAuditLogCommandHanlder _createAuditLogCommandHanlder;
        private readonly IGetAuditLogByUserQueryHandler _getAuditLogByUserQueryHandler;

        public AuditLogsController(ICreateAuditLogCommandHanlder createAuditLogCommandHanlder, IGetAuditLogByUserQueryHandler getAuditLogByUserQueryHandler)
        {
            _createAuditLogCommandHanlder = createAuditLogCommandHanlder;
            _getAuditLogByUserQueryHandler = getAuditLogByUserQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser([FromQuery] GetAuditLogByUserQuery query)
        {
            var result = await _getAuditLogByUserQueryHandler.Handler(query);
            return Ok(result);
        }
    }
}
