

using Application.DTOs.Reservation;
using Application.Interfaces.Reservations;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.UseCase.Commands.Reservation
{
    public class ConfirmReservationHandler : IConfirmReservationHandler
    {
        private readonly IReservationRepository _repository;

        public ConfirmReservationHandler(IReservationRepository reservationRepository)
        {
            _repository = reservationRepository;
        }

        public async Task<ReservationResponse> Handle(ConfirmReservationCommand command)
        {
            var _reservation = await _repository.GetByIdAsync(command.ReservationId);
            if (_reservation == null)
            {
                throw new ReservationNotFoundException("La reserva no fue encontrada");
            }

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
