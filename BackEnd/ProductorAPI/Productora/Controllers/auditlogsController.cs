using Application.DTOs;
using Application.DTOs.AuditLogs;
using Application.DTOs.Users;
using Application.Interfaces.AuditLogs;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Queries.AuditLogs;
using Microsoft.AspNetCore.Mvc;
using Productora.Documentation.SwaggerExamples.AuditLogs;
using Productora.Documentation.SwaggerExamples.Errors;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

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
        [ProducesResponseType(typeof(AuditLogResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

        [SwaggerResponse(200, "OK", typeof(AuditLogResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(ErrorResponseDTO))]
        [SwaggerResponse(404, "User not found", typeof(ErrorResponseDTO))]

        [SwaggerResponseExample(200, typeof(AuditLogResponse))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(404, typeof(AuditLogNotFoundExample))]

        public async Task<IActionResult> GetByUser(int userId)
        {
            var result = await _getAuditLogByUserQueryHandler.Handler(new GetAuditLogByUserQuery { UserId = userId});
            return Ok(result);
        }
    }
}
