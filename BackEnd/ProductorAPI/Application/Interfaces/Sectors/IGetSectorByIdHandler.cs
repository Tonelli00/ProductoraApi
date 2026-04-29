using Application.DTOs;
using Application.UseCase.Queries.Events;
using Application.UseCase.Queries.Sectors;

namespace Application.Interfaces.Sectors;

public interface IGetSectorByIdHandler
{
    Task<SectorResponseDTO> Handle(GetSectorByIdQuery query);
}