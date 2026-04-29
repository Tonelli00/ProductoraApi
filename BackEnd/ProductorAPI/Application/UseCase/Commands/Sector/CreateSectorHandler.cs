using Application.DTOs;
using Application.Interfaces.Events;
using Application.Interfaces.Seats;
using Application.Interfaces.Sectors;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Queries.Events;
using Domain.Exceptions;

namespace Application.UseCase.Commands.Sector;

public class CreateSectorHandler:ICreateSectorHandler
{
    private readonly ISectorRepository _sectorRepository;
    private readonly IGetEventByIdHandler _getEventByIdHandler;
    private readonly ICreateSeatsHandler _createSeatsHandler;

    public CreateSectorHandler(ISectorRepository sectorRepository, IGetEventByIdHandler getEventByIdHandler, ICreateSeatsHandler createSeatsHandler)
    {
        _sectorRepository = sectorRepository;
        _getEventByIdHandler = getEventByIdHandler;
        _createSeatsHandler = createSeatsHandler;
    }

    public async Task<SectorShortResponseDTO> Handle(CreateSectorCommand command)
    {
        var _event = await _getEventByIdHandler.Handle(new GetEventByIdQuery { EventId = command.EventId }) ?? throw new EventNotFoundException("El evento no fue encontrado");


        var _sector = new Domain.Entities.Sector
        {
            EventId = command.EventId,
            Name = command.Name,
            Price = command.Price,
            Capacity = command.Capacity,
            Seats = new List<Domain.Entities.Seat>()
        };

        var InsertedSector = await _sectorRepository.InsertAsync(_sector);

        await _createSeatsHandler.Handle(new CreateSeatsCommand
            { SectorId = InsertedSector.Id, SeatsToCreate = command.Capacity });

    

        return new SectorShortResponseDTO()
        {
            SectorId = InsertedSector.Id,
            Name = InsertedSector.Name,
            Capacity = InsertedSector.Capacity,
            Price = InsertedSector.Price
        };

    }
}