using Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Documentation.SwaggerExamples.Seats
{
    public class SeatConcurrenceExample : IExamplesProvider<ErrorResponseDTO>
    {
        public ErrorResponseDTO GetExamples()
        {
            return new ErrorResponseDTO
            {
                StatusCode = 409,
                Message = "El asiento ya ha sido reservado por otro usuario."
            };
        }
    }
}
