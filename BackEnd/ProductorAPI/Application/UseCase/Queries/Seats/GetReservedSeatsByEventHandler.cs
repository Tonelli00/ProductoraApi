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

        public async Task<IEnumerable<SeatResponse>> Handle(GetReservedSeatsByEventIdQuery Query)
        {
            var seats = await _repository.GetReservedSeatsByEventId(Query.EventId);
            return seats.Select(s => new SeatResponse
            {
                SectorId = s.SectorId,
                SeatNumber = s.SeatNumber,
                RowIdentifier = s.RowIdentifier,
                Status = s.Status,
            }).ToList();
        }
    }
}
