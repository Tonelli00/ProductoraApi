using Application.DTOs;
using Application.DTOs.Seat;
using Application.Interfaces.Sectors;
using Domain.Exceptions;

namespace Application.UseCase.Queries.Sectors;

public class GetSectorByIdHandler : IGetSectorByIdHandler
{
    private readonly ISectorRepository _repository;

    public GetSectorByIdHandler(ISectorRepository repository)
    {
        _repository = repository;
    }

    public async Task<SectorResponseDTO> Handle(GetSectorByIdQuery query)
    {
        if(query.SectorId==0 || query.SectorId < 0) 
        {
            throw new ArgumentException("Ingrese valores válidos");
        }

        var sector = await _repository.GetSectorByIdAsync(query.SectorId) ?? throw new SectorNotFoundException("Sector no encontrado");
        return new SectorResponseDTO()
        {
            SectorId = sector.Id,
            Capacity = sector.Capacity,
            Name = sector.Name,
            Price = sector.Price,
            Seats = sector.Seats.Select(s => new SeatResponseDTO()
            {
                SeatId = s.Id,
                RowIdentifier = s.RowIdentifier,
                SeatNumber = s.SeatNumber,
                SectorId=s.SectorId,
                Status = s.Status
            }).ToList()
        };
        
    }

}