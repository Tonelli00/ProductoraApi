
using Application.Interfaces;
using Application.Models.Responses;

namespace Application.UseCase.Seat
{
    public class SeatService : ISeatServices
    {
        private readonly ISeatQuery _query;
        private readonly ISeatCommand _command;

        public SeatService(ISeatQuery query, ISeatCommand command)
        {
            _query = query;
            _command = command;
        }

        public async Task<IEnumerable<SeatResponseDTO>> GetAllSeats()
        {
            var seats = await _query.GetAllSeatsAsync();
            return seats.Select(s => new SeatResponseDTO {
            SeatId = s.Id,
            SectorId = s.SectorId,
            RowIdentifier = s.RowIdentifier,
            SeatNumber = s.SeatNumber,
            Status = s.Status,
            }).ToList();
        }
    }
}
