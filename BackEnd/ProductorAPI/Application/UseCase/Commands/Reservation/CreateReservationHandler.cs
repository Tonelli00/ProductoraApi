using Application.DTOs.Reservation;
using Application.Interfaces.AuditLogs;
using Application.Interfaces.Reservations;
using Application.Interfaces.Seats;
using Application.Interfaces.Users;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Queries.Seats;
using Application.UseCase.Queries.Users;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Exceptions.Users;

namespace Application.UseCase.Commands.Reservation
{
    public class CreateReservationHandler : ICreateReservationCommandHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGetSeatByIdHandler _getSeatByIdHandler;
        private readonly IMarkSeatAsReservedHandler _markSeatAsReserverHandler;
        private readonly ICreateAuditLogCommandHanlder _createAuditLogCommandHandler;
        private readonly IGetUserByIdHandler _getUserByIdQueryHandler;
        public CreateReservationHandler(IReservationRepository reservationRepository, IGetSeatByIdHandler getSeatByIdHandler, IMarkSeatAsReservedHandler markSeatAsReserverHandler,
            ICreateAuditLogCommandHanlder createAuditLogCommandHanlder, IGetUserByIdHandler getUserByIdQueryHandler)
        {
            _reservationRepository = reservationRepository;
            _getSeatByIdHandler = getSeatByIdHandler;
            _markSeatAsReserverHandler = markSeatAsReserverHandler;
            _createAuditLogCommandHandler = createAuditLogCommandHanlder;
            _getUserByIdQueryHandler = getUserByIdQueryHandler;
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

            // validar asiento
            var seat = await _getSeatByIdHandler.Handle(new GetSeatByIdQuery { SeatId = command.SeatId});
            if(seat == null)
                throw new SeatNotFoundException("El asiento no existe");

            // validar disponibilidad
            if(seat.Status == "Reserved" || seat.Status == "Sold")
            {
                await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                {
                    UserId = command.UserId,
                    Action = AuditAction.RESERVE_ATTEMPT.ToString(),
                    EntityType = "Seat",
                    EntityId = seat.Id.ToString(),
                    Details = $"Intentó reservar el asiento {seat.SeatNumber} en el sector {seat.Sector.Name}, pero ya estaba reservado o vendido"
                });
                throw new SectorConflictException("El asiento ya está reservado, intente con otro.");
            }


            // inicio de la transacción
            await using var transaction = await _reservationRepository

            try
            {
                // crear reserva
                var reservation = new Domain.Entities.Reservation
                {
                    UserId = command.UserId,
                    SeatId = seat.Id,
                    Status = "Pending",
                    ReservedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                };

                // guardar cambios
                await _reservationRepository.CreateReservationAsync(reservation);

                // actualizar estado del asiento
                MarkSeatAsReservedCommand seatAsReserved = new MarkSeatAsReservedCommand { SeatNumber = seat.SeatNumber, SectorId = seat.SectorId };
                await _markSeatAsReserverHandler.Handle(seatAsReserved);

                // crear el log de auditoría
                await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
                {
                    UserId = command.UserId,
                    Action = AuditAction.RESERVE_SUCCESS.ToString(),
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Reserva creada para el asiento {seat.SeatNumber} en el sector {seat.Sector.Name}"
                });



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
            catch (Exception)
            {

            }
        }
    }
}
