using Application.DTOs;
using Domain.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EventNotFoundException ex)
        {
            try
            {
                await delegateNext(httpContext);
            }
            catch (ArgumentException ex)
            {               
                await StatusMessage(httpContext, 400, ex.Message);
            }
            catch (SeatNotFoundException ex) 
            {               
                await StatusMessage(httpContext, 404, ex.Message);
            }
            catch (EventNotFoundException ex) 
            {
                await StatusMessage(httpContext, 404, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                await StatusMessage(httpContext, 404, ex.Message);
            }
            catch (SectorNotFoundException ex) 
            {
                await StatusMessage(httpContext, 404, ex.Message);
            }
            catch (SectorConflictException ex) 
            {               
                await StatusMessage(httpContext, 409, ex.Message);
            }
            catch (EmailConflictException ex)
            {
                await StatusMessage(httpContext, 409, ex.Message);
            }

            catch (PasswordConflictException ex)
            {
                await StatusMessage(httpContext, 409, ex.Message);
            }
            catch (FullSectorException ex) 
            {
                await StatusMessage(httpContext, 409, ex.Message);
            }
            
            catch (ReservedSeatException ex) 
            {
                await StatusMessage(httpContext, 409, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                await StatusMessage(httpContext, 404, ex.Message);
            }
            catch(ReservationNotFoundException ex)
            {
                await StatusMessage(httpContext, 404, ex.Message);
            }

            catch (Exception ex)
            {
                await StatusMessage(httpContext, 500, "Internal server error");
            }
        }
    }

    private static async Task Handle(HttpContext context, int statusCode, string code, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        private async Task StatusMessage(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new { statusCode, message };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }
}