using Application.Interfaces.Events;
using Application.Interfaces.Seats;
using Application.Interfaces.Sectors;
using Application.UseCase.Queries.Events;
using Application.UseCase.Queries.Sectors;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.UseCase.Commands.Seat;

public class CreateSeatsHandler:ICreateSeatsHandler
{
    private readonly ISeatRepository _seatRepository;
  
    private readonly IGetSectorByIdHandler _getSectorByIdHandler;
    public CreateSeatsHandler(ISeatRepository seatRepository,IGetSectorByIdHandler getSectorByIdHandler)
    {
        _seatRepository = seatRepository;
     
        _getSectorByIdHandler = getSectorByIdHandler;
    }

    public async Task Handle(CreateSeatsCommand command)
    {
      
        var _sector = await _getSectorByIdHandler.Handle(new GetSectorByIdQuery { SectorId = command.SectorId }) ?? throw new SectorNotFoundException("Sector no encontrado.");
        
        
        int currentCount = _sector.Seats?.Count() ?? 0;
        if (currentCount + command.SeatsToCreate > _sector.Capacity)
        {
            int available = _sector.Capacity - currentCount;
            throw new FullSectorException($"No se pueden agregar más de {available} asientos a este sector..");
        }
        
        
        var seats = new List<Domain.Entities.Seat>();

        int seatsPerRow = 10; 
        
        for (int i = 0; i < command.SeatsToCreate; i++)
        {
            int index = currentCount + i;

            char row = (char)('A' + (index / seatsPerRow));
            int number = (index % seatsPerRow) + 1;

            var seat = new Domain.Entities.Seat
            {
                SectorId = _sector.SectorId,
                RowIdentifier = row.ToString(),
                SeatNumber = number,
                Status = "Available"
            };

            seats.Add(seat);
        }

        await _seatRepository.InsertSeatsAsync(seats);
        
    }
}