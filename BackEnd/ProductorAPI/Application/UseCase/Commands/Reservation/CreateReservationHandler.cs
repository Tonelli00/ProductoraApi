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
        public CreateReservationHandler(IReservationRepository reservationRepository, IGetSeatByIdHandler getSeatByIdHandler, IMarkSeatAsReservedHandler markSeatAsReserverHandler,
            ICreateAuditLogHanlder createAuditLogCommandHanlder, IGetUserByIdHandler getUserByIdQueryHandler,IGetSectorByIdHandler getSectorById,IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _getSeatByIdHandler = getSeatByIdHandler;
            _markSeatAsReserverHandler = markSeatAsReserverHandler;
            _createAuditLogCommandHandler = createAuditLogCommandHanlder;            
            _getUserByIdQueryHandler = getUserByIdQueryHandler;
            _getSeatByIdHandler= getSeatByIdHandler;
            _getSectorById= getSectorById;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationResponse> Handle(CreateReservationCommand command)
        {
            // validar datos
            if(command.UserId <= 0 || command.UserId.Equals(null) )
                throw new ArgumentException("El UserId es requerido");
            
            if (command.SeatId == Guid.Empty)
                throw new ArgumentException("Seleccione un asiento válido");
            
            // validar usuario
              GetUserByIdQuery query =new GetUserByIdQuery { UserId = command.UserId };
              var user = await _getUserByIdQueryHandler.Handler(query);
              if(user == null)
                  throw new UserNotFoundException("El usuario no existe");            

            // iniciar transacción
            await _unitOfWork.BeginTransactionAsync();

            var seat = await _getSeatByIdHandler.Handle(new GetSeatByIdQuery { SeatId = command.SeatId });
            
            if (seat == null) 
            {
                throw new SeatNotFoundException("El asiento no fue encontrado");
            }

            var sector = await _getSectorById.Handle(new GetSectorByIdQuery { SectorId = seat.SectorId});

            try
            {
                // validar asiento
                if (seat == null)
                    throw new SeatNotFoundException("El asiento no existe");

                // validar disponibilidad
                if (seat.Status == "Reserved" || seat.Status == "Sold")
                {
                    await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                    {
                        UserId = command.UserId,
                        Action = AuditAction.RESERVE_ATTEMPT.ToString(),
                        EntityType = "Seat",
                        EntityId = seat.SeatId.ToString(),
                        Details = $"Intentó reservar el asiento {seat.SeatNumber} en el sector {sector.Name}, pero ya estaba reservado o vendido"
                    });
                    throw new SectorConflictException("El asiento ya está reservado, intente con otro.");
                }

                // crear reserva
                var argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Argentina/Buenos_Aires");
                var reservation = new Domain.Entities.Reservation
                {
                    UserId = command.UserId,
                    SeatId = seat.SeatId,
                    Status = "Pending",
                    ReservedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, argentinaTimeZone),
                    ExpiresAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(5), argentinaTimeZone),
                };

                // guardar cambios
                await _reservationRepository.CreateReservationAsync(reservation);

                // actualizar estado del asiento               
                await _markSeatAsReserverHandler.Handle(new MarkSeatAsReservedCommand
                {
                    SeatId = seat.SeatId
                });

                // crear el log de auditoría
                await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                {
                    UserId = command.UserId,
                    Action = AuditAction.RESERVE_SUCCESS.ToString(),
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Reserva creada para el asiento {seat.SeatNumber} en el sector {sector.Name}"
                });    

                // Confirmar la Transacción
                await _unitOfWork.CommitAsync();               

                // retornar respuesta
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
            // atrapar exception de auditoría 
            catch
            {
                await _unitOfWork.RollBackAsync();
                await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                {
                    UserId= command.UserId,
                    Action = AuditAction.RESERVE_ATTEMPT.ToString(),
                    EntityType = "Seat",
                    EntityId = seat.SeatId.ToString(),
                    Details = $"El usuario {command.UserId} intentó reservar el asiento {seat.SeatNumber} en el sector {sector.Name}, pero ya estaba reservado"
                });
                throw;
            }                        
        }
    }
}
