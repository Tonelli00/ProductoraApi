using Application.DTOs;
using Domain.Exceptions;
using Domain.Exceptions.Reservations;
using Domain.Exceptions.Seats;
using Domain.Exceptions.Users;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            await StatusMessage(context, 400, ex.Message, ex);
        }
        catch (SeatNotFoundException ex)
        {
            await StatusMessage(context, 404, ex.Message, ex);
        }
        catch (SeatConcurrenceException ex)
        {
            await StatusMessage(context, 409, ex.Message, ex);
        }
        catch (EventNotFoundException ex)
        {
            await StatusMessage(context, 404, ex.Message, ex);
        }
        catch (UserNotFoundException ex)
        {
            await StatusMessage(context, 404, ex.Message, ex);
        }
        catch (SectorNotFoundException ex)
        {
            await StatusMessage(context, 404, ex.Message, ex);
        }
        catch (ReservationNotFoundException ex)
        {
            await StatusMessage(context, 404, ex.Message, ex);
        }
        catch (CancelReservationFailException ex)
        {
            await StatusMessage(context, 409, ex.Message, ex);
        }
        catch (KeyNotFoundException ex)
        {
            await StatusMessage(context, 404, ex.Message, ex);
        }
        catch (SectorConflictException ex)
        {
            await StatusMessage(context, 409, ex.Message, ex);
        }
        catch (EmailConflictException ex)
        {
            await StatusMessage(context, 409, ex.Message, ex);
        }
        catch (PasswordConflictException ex)
        {
            await StatusMessage(context, 409, ex.Message, ex);
        }
        catch (FullSectorException ex)
        {
            await StatusMessage(context, 409, ex.Message, ex);
        }
        catch (ReservedSeatException ex)
        {
            await StatusMessage(context, 409, ex.Message,ex);
        }
        catch (Exception ex)
        {
            await StatusMessage(context, 500, "Internal server error",ex);
        }
    }

    private async Task StatusMessage(HttpContext context, int statusCode, string message,Exception ex)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        _logger.LogError(ex,"Ocurrió un error");
        var response = new ErrorResponseDTO
        {
            StatusCode = statusCode,
            Message = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}