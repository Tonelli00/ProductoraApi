using Application.DTOs.Reservation;
using Application.Interfaces.AuditLogs;
using Application.Interfaces.Reservations;
using Application.Interfaces.Seats;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Queries.Seats;
using Domain.Enums;

namespace Application.UseCase.Commands.Reservation
{
    public class CancelReservationHandler : ICancelReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGetSeatByIdHandler _getSeatByIdHandler;
        private readonly IMarkSeatAtAvailableHandler _markSeatAtAvailableHandler;
        private readonly ICreateAuditLogHanlder _createAuditLogHanlder;

        public CancelReservationHandler(IReservationRepository reservationRepository, IGetSeatByIdHandler getSeatByIdHandler
            , IMarkSeatAtAvailableHandler markSeatAtAvailableHandler, ICreateAuditLogHanlder createAuditLogHanlder)
        {
            _reservationRepository = reservationRepository;
            _getSeatByIdHandler = getSeatByIdHandler;
            _markSeatAtAvailableHandler = markSeatAtAvailableHandler;
            _createAuditLogHanlder = createAuditLogHanlder;
        }

        public async Task Handler(CancelReservationCommand command)
        {
            var reservation = await _reservationRepository.GetByIdAsync(command.ReservationId);

            // cambiar estado de asiento
            var Seat = await _getSeatByIdHandler.Handle(new GetSeatByIdQuery { SeatId = reservation.SeatId });
            await _markSeatAtAvailableHandler.Handle(new MarkSeatAsAvailableCommand { SeatId = Seat.SeatId });

            // cambiar estado de reserva 
            reservation.Status = "Expired";
            await _reservationRepository.CancelReservation(reservation);

            // crear auditoria
            await _createAuditLogHanlder.Handler(new CreateAuditLogCommand
            {
                UserId = reservation.UserId,
                Action = AuditAction.EXPIRED.ToString(),
                EntityType = "Reservation",
                EntityId = reservation.Id.ToString(),
                Details = $"Se vencio el tiempo de la reserva {reservation.Id} para le asiento {Seat.SeatId}"
            });
        }
    }
}
