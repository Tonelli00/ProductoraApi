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

namespace Application.UseCase.Commands.Reservation
{
    public class CreateReservationHandler : ICreateReservationCommandHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGetSeatByIdHandler _getSeatByIdHandler;
        private readonly IMarkSeatAsReservedCommandHandler _markSeatAsReserverHandler;
        private readonly ICreateAuditLogCommandHanlder _createAuditLogCommandHandler;
        private readonly IGetUserByIdQueryHandler _getUserByIdQueryHandler;
        public CreateReservationHandler(IReservationRepository reservationRepository, IGetSeatByIdHandler getSeatByIdHandler, IMarkSeatAsReservedCommandHandler markSeatAsReserverHandler,
            ICreateAuditLogCommandHanlder createAuditLogCommandHanlder, IGetUserByIdQueryHandler getUserByIdQueryHandler)
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
                  throw new Exception("El usuario no existe");

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
                    Action = AuditAction.RESERVER_ATTEMPT.ToString(),
                    EntityType = "Seat",
                    EntityId = seat.Id.ToString(),
                    Details = $"Intentó reservar el asiento {seat.SeatNumber} en el sector {seat.SectorId}, pero ya estaba reservado o vendido"
                });
                throw new Exception("El asiento ya está reservado");
            }                


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
            // actualizar estado del asiento
            await _reservationRepository.CreateReservationAsync(reservation);
            MarkSeatAsReservedCommand seatAsReserved = new MarkSeatAsReservedCommand { SeatNumber = seat.SeatNumber, SectorId = seat.SectorId};
            await _markSeatAsReserverHandler.Handle(seatAsReserved);

            // crear el log de auditoría
            await _createAuditLogCommandHandler.Handler(new CreateAuditLogCommand
            {
                UserId = command.UserId,
                Action = AuditAction.RESERVER_SUCCESS.ToString(),
                EntityType = "Reservation",
                EntityId = reservation.Id.ToString(),
                Details = $"Reserva creada para el asiento {seat.SeatNumber} en el sector {seat.SectorId}"
            });

            // retornar respuesta
            return new ReservationResponse
            {
                Id = reservation.Id,
                Status = reservation.Status,
                ReservedAt = reservation.ReservedAt
            };
        }
    }
}
