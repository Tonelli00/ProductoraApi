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
            await Handle(context, 404, "EVENT_NOT_FOUND", ex.Message);
        }
        catch (SeatNotFoundException ex)
        {
            await Handle(context, 404, "SEAT_NOT_FOUND", ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            await Handle(context, 404, "USER_NOT_FOUND", ex.Message);
        }
        catch (SectorNotFoundException ex)
        {
            await Handle(context, 404, "SECTOR_NOT_FOUND", ex.Message);
        }

        catch (EmailConflictException ex)
        {
            await Handle(context, 409, "EMAIL_CONFLICT", ex.Message);
        }
        catch (PasswordConflictException ex)
        {
            await Handle(context, 409, "PASSWORD_CONFLICT", ex.Message);
        }
        catch (SectorConflictException ex)
        {
            await Handle(context, 409, "SECTOR_CONFLICT", ex.Message);
        }
        catch (FullSectorException ex)
        {
            await Handle(context, 409, "SECTOR_FULL", ex.Message);
        }
        catch (ReservedSeatException ex)
        {
            await Handle(context, 409, "SEAT_RESERVED", ex.Message);
        }

        catch (ArgumentException ex)
        {
            await Handle(context, 400, "BAD_REQUEST", ex.Message);
        }

        catch (Exception ex)
        {
            await Handle(context, 500, "INTERNAL_ERROR", "Internal server error");
        }
    }

    private static async Task Handle(HttpContext context, int statusCode, string code, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new ErrorReponseDTO
        {
            Status=statusCode,
            Code = code,
            Message = message
        });
    }
}