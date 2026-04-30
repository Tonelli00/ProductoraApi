using Domain.Exceptions;
using System.Text.Json;

namespace Productora.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate delegateNext;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate delegateNext, ILogger<ExceptionMiddleware> logger)
        {
            this.delegateNext = delegateNext;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await delegateNext(httpContext);
            }
            catch (ArgumentException ex)
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 400, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (SeatNotFoundException ex) 
            {
                httpContext.Response.StatusCode = 404;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 404, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (EventNotFoundException ex) 
            {
                httpContext.Response.StatusCode = 404;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 404, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (UserNotFoundException ex)
            {
                httpContext.Response.StatusCode = 404;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 404, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (SectorNotFoundException ex) 
            {
                httpContext.Response.StatusCode = 404;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 404, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (SectorConflictException ex) 
            {
                httpContext.Response.StatusCode = 409;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 409, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (EmailConflictException ex)
            {
                httpContext.Response.StatusCode = 409;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 409, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }

            catch (PasswordConflictException ex)
            {
                httpContext.Response.StatusCode = 409;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 409, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (FullSectorException ex) 
            {
                httpContext.Response.StatusCode = 409;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 409, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            
            catch (ReservedSeatException ex) 
            {
                httpContext.Response.StatusCode = 409;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 409, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (KeyNotFoundException ex)
            {
                httpContext.Response.StatusCode = 404;
                httpContext.Response.ContentType = "application/json";

                var response = new { statusCode = 404, message = ex.Message };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }

            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                logger.LogError(ex, "Unhandled exception");

                var response = new { statusCode = 500, message = "Internal server error" };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }

    }
}
