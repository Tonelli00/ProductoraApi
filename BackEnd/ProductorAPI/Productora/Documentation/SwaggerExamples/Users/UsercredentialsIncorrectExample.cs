using Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Documentation.SwaggerExamples.Users
{
    public class UsercredentialsIncorrectExample :IExamplesProvider<ErrorResponseDTO>
    {
        public ErrorResponseDTO GetExamples()
        {
            return new ErrorResponseDTO
            {
                StatusCode = 401,
                Message = "Las credenciales proporcionadas son incorrectas."
            };
        }
    }
}
