
using Application.DTOs;
using Application.Interfaces.Sectors;

namespace Application.UseCase.Queries.Sectors
{
    public class GetSectorSummaryHandler : IGetSectorSummaryHandler
    {
        private readonly ISectorRepository sectorRepository;

        public GetSectorSummaryHandler(ISectorRepository sectorRepository)
        {
            this.sectorRepository = sectorRepository;
        }

        public async Task<SectorShortResponseDTO> Handle(GetSectorSummaryQuery query)
        {
            var sector = await sectorRepository.GetSectorSummaryByIdAsync(query.SectorId);
            return new SectorShortResponseDTO
            {
                SectorId = sector.Id,
                EventId = sector.EventId,
                Name = sector.Name,
                Capacity = sector.Capacity,
                Price = sector.Price,
            };
        }
    }
}
