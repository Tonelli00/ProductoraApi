using Application.Interfaces.Seats;
using Domain.Exceptions;
using Domain.Entities;

namespace Application.UseCase.Commands.Seat
{
    public class MarkSeatAsReservedHandler : IMarkSeatAsReservedCommandHandler
    {
        private readonly ISeatRepository _repository;
        

        public MarkSeatAsReservedHandler(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(MarkSeatAsReservedCommand command)
        {
            if (command.SectorId <= 0 || command.SeatNumber <= 0)
            {
                throw new ArgumentException("Los valores ingresados deben ser mayores a 0");
            }

            var seat = await _repository.GetSeatBySeatNumberAndSectorId(command.SeatNumber,command.SectorId);
            if (seat == null) 
            {
                throw new SeatNotFoundException("El asiento seleccionado no existe...");
            }
            
            //Validación para ver si es que el sector existe

            seat.Status="Reserved";

            await _repository.UpdateSeatStatus(seat);               
        }
    }
}
