using Application.DTOs.Reservation;
using Application.Interfaces;
using Application.Interfaces.AuditLogs;
using Application.Interfaces.Reservations;
using Application.Interfaces.Seats;
using Application.Interfaces.Sectors;
using Application.Interfaces.Users;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Queries.Seats;
using Application.UseCase.Queries.Sectors;
using Application.UseCase.Queries.Users;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Exceptions.Seats;
using Domain.Exceptions.Users;

namespace Application.UseCase.Commands.Reservation
{
    public class CreateReservationHandler : ICreateReservationCommandHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGetSeatByIdHandler _getSeatByIdHandler;
        private readonly IMarkSeatAsReservedHandler _markSeatAsReserverHandler;
        private readonly ICreateAuditLogHanlder _createAuditLogCommandHandler;
        private readonly IGetUserByIdHandler _getUserByIdQueryHandler;
        private readonly IGetSectorByIdHandler _getSectorById;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationHandler(
            IReservationRepository reservationRepository,
            IGetSeatByIdHandler getSeatByIdHandler,
            IMarkSeatAsReservedHandler markSeatAsReserverHandler,
            ICreateAuditLogHanlder createAuditLogCommandHanlder,
            IGetUserByIdHandler getUserByIdQueryHandler,
            IGetSectorByIdHandler getSectorById,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _getSeatByIdHandler = getSeatByIdHandler;
            _markSeatAsReserverHandler = markSeatAsReserverHandler;
            _createAuditLogCommandHandler = createAuditLogCommandHanlder;
            _getUserByIdQueryHandler = getUserByIdQueryHandler;
            _getSectorById = getSectorById;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationResponse> Handle(CreateReservationCommand command)
        {
            if (command.UserId <= 0)
                throw new ArgumentException("El UserId es requerido");

            if (command.SeatId == Guid.Empty)
                throw new ArgumentException("Seleccione un asiento válido");

            var user = await _getUserByIdQueryHandler.Handler(
                new GetUserByIdQuery { UserId = command.UserId });

            if (user == null)
                throw new UserNotFoundException("El usuario no existe");

            await _unitOfWork.BeginTransactionAsync();

            var seat = await _getSeatByIdHandler.Handle(
                new GetSeatByIdQuery { SeatId = command.SeatId });

            if (seat == null)
                throw new SeatNotFoundException("El asiento no fue encontrado");

            var sector = await _getSectorById.Handle(
                new GetSectorByIdQuery { SectorId = seat.SectorId });

            try
            {
                if (seat.Status == "Reserved" || seat.Status == "Sold")
                    throw new SectorConflictException("El asiento ya está reservado, intente con otro.");

                var argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Argentina/Buenos_Aires");

                var reservation = new Domain.Entities.Reservation
                {
                    UserId = command.UserId,
                    SeatId = seat.SeatId,
                    Status = "Pending",
                    ReservedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, argentinaTimeZone),
                    ExpiresAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(5), argentinaTimeZone),
                };

                await _reservationRepository.CreateReservationAsync(reservation);

                await _markSeatAsReserverHandler.Handle(
                    new MarkSeatAsReservedCommand { SeatId = seat.SeatId });

                await _unitOfWork.CommitAsync();

                await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                {
                    UserId = command.UserId,
                    Action = AuditAction.RESERVE_SUCCESS.ToString(),
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Reserva creada para el asiento {seat.SeatNumber} en el sector {sector.Name}"
                });

                return new ReservationResponse
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    SeatId = reservation.SeatId,
                    Status = reservation.Status,
                    ReservedAt = reservation.ReservedAt,
                    ExpiresAt = reservation.ExpiresAt,
                };
            }
            catch (SectorConflictException)
            {
                await _unitOfWork.RollBackAsync();

                await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                {
                    UserId = command.UserId,
                    Action = AuditAction.RESERVE_ATTEMPT.ToString(),
                    EntityType = "Seat",
                    EntityId = seat.SeatId.ToString(),
                    Details = $"Intentó reservar el asiento {seat.SeatNumber} en el sector {sector.Name}, pero ya estaba reservado o vendido"
                });

                throw;
            }
          
            catch (SeatConcurrenceException)
            {
                await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                {
                    UserId = command.UserId,
                    Action = AuditAction.RESERVE_ATTEMPT.ToString(),
                    EntityType = "Seat",
                    EntityId = seat.SeatId.ToString(),
                    Details = $"El asiento {seat.SeatNumber} fue tomado por otro usuario en el sector {sector.Name}"
                });

                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackAsync();

                await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                {
                    UserId = command.UserId,
                    Action = AuditAction.RESERVE_ATTEMPT.ToString(),
                    EntityType = "Seat",
                    EntityId = seat.SeatId.ToString(),
                    Details = $"Ocurrió un error inesperado al reservar el asiento {seat.SeatNumber}"
                });

                throw;
            }
        }
    }
}