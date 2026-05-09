using Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Documentation.SwaggerExamples.Seats
{
    public class SeatNotFoundExample : IExamplesProvider<ErrorResponseDTO>
    {
        public ErrorResponseDTO GetExamples()
        {
            return new ErrorResponseDTO
            {
                StatusCode = 404,
                Message = "No se encontro el asiento."
            };
        }
    }
}
