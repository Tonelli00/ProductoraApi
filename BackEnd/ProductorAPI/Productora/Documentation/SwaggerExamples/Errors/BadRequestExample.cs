using Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Documentation.SwaggerExamples.Errors
{
    public class BadRequestExample : IExamplesProvider<ErrorResponseDTO>
    {
        public ErrorResponseDTO GetExamples()
        {
            return new ErrorResponseDTO
            {
                StatusCode = 400,
                Message = "Los datos de entrada son invalidos."
            };
        }
    }
}
