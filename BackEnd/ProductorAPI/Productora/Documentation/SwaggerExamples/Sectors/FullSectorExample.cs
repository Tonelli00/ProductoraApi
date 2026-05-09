using Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Documentation.SwaggerExamples.Sectors
{
    public class FullSectorExample : IExamplesProvider<ErrorResponseDTO>
    {
        public ErrorResponseDTO GetExamples()
        {
            return new ErrorResponseDTO
            {
               StatusCode = 400,
               Message = "El sector esta lleno."
            };
        }
    }
}
