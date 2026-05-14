

using Application.DTOs.Reservation;
using Application.Interfaces.Reservations;
using Application.Interfaces.Seats;
using Domain.Exceptions;

namespace Application.UseCase.Commands.Reservation
{
    public class ConfirmReservationHandler : IConfirmReservationHandler
    {
        private readonly IReservationRepository _repository;
        private readonly IMarkSeatAsSoldHandler _markSeatAsSoldHandler;
        public ConfirmReservationHandler(IReservationRepository reservationRepository, IMarkSeatAsSoldHandler markSeatAsSoldHandler)
        {
            _repository = reservationRepository;
            _markSeatAsSoldHandler = markSeatAsSoldHandler;
        }

        public async Task<ReservationResponse> Handle(ConfirmReservationCommand command)
        {
            var _reservation = await _repository.GetByIdAsync(command.ReservationId);
            if (_reservation == null)
            {
                throw new ReservationNotFoundException("La reserva no fue encontrada");
            }

            _reservation.Status = "Paid";

            await _markSeatAsSoldHandler.Handle(new Seat.MarkSeatAsSoldCommand { SeatId = _reservation.SeatId });

            var result = await _repository.ConfirmReservationAsync(_reservation);

            return new ReservationResponse
            {
                Id = result.Id,
                UserId = result.UserId,
                SeatId = result.SeatId,
                Status = result.Status,
                ReservedAt = result.ReservedAt,
                ExpiresAt = result.ExpiresAt,
            };

        }
    }
}
