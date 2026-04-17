
using Application.Interfaces;
using Application.Models.Responses;

namespace Application.UseCase.Sector
{
    public class SectorService : ISectorServices
    {
        private readonly ISectorQuery _query;
        private readonly ISectorCommand _command;

        public SectorService(ISectorQuery query, ISectorCommand command)
        {
            _query = query;
            _command = command;
        }

        public async Task<IEnumerable<SeatResponseDTO>> GetSeatsBySectorId(int SectorId)
        {
            var seats = await _query.GetAllSeatsBySectorId(SectorId);
            return seats.Select(s=> new SeatResponseDTO 
            {
            SeatId=s.Id,
            SectorId=s.SectorId,
            RowIdentifier=s.RowIdentifier,
            SeatNumber=s.SeatNumber,
            Status=s.Status,
            }).ToList();
        }
    }
}
