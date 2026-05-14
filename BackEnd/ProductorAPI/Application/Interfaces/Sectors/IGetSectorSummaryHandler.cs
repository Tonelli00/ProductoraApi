

using Application.DTOs;
using Application.UseCase.Queries.Sectors;

namespace Application.Interfaces.Sectors
{
    public interface IGetSectorSummaryHandler
    {
        Task<SectorShortResponseDTO> Handle(GetSectorSummaryQuery query);
    }
}
