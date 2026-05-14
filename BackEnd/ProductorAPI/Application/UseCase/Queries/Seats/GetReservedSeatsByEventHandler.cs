using Application.DTOs.Seat;
using Application.Interfaces.Seats;

namespace Application.UseCase.Queries.Seats
{
    public class GetReservedSeatsByEventHandler : IGetReservedSeatsByEventHandler
    {
        private readonly ISeatRepository _repository;

        public GetReservedSeatsByEventHandler(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SeatResponseDTO>> Handle(GetReservedSeatsByEventIdQuery Query)
        {
            var seats = await _repository.GetReservedSeatsByEventId(Query.EventId);
            return seats.Select(s => new SeatResponseDTO
            {
                SeatId = s.Id,
                SeatNumber = s.SeatNumber,
                SectorId = s.SectorId,
                RowIdentifier = s.RowIdentifier,
                Status = s.Status,
            }).ToList();
        }
    }
}
