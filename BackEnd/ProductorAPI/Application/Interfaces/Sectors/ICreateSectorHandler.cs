using Application.DTOs;
using Application.UseCase.Commands.Sector;

namespace Application.Interfaces.Sectors;

public interface ICreateSectorHandler
{
    Task<SectorShortResponseDTO> Handle(CreateSectorCommand command);
}