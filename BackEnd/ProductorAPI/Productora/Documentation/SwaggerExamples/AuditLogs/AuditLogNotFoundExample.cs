using Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Documentation.SwaggerExamples.AuditLogs
{
    public class AuditLogNotFoundExample : IExamplesProvider<ErrorResponseDTO>
    {
        public ErrorResponseDTO GetExamples()
        {
            return new ErrorResponseDTO
            {
                StatusCode = 404,
                Message = "No se encontro el registro de auditoría."
            };
        }
    }
}
