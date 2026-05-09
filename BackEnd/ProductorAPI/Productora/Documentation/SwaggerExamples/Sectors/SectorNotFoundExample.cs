using Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Documentation.SwaggerExamples.Sectors
{
    public class SectorNotFoundExample : IExamplesProvider<ErrorResponseDTO>
    {
        public ErrorResponseDTO GetExamples()
        {
            return new ErrorResponseDTO
            {
                StatusCode = 404,
                Message = "No se encontro el sector."
            };
        }
    }
}
