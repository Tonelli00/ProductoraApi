using Application.DTOs.Seat;
using Application.Interfaces.Seats;
using Application.UseCase.Queries.Seats;


namespace Application.UseCase.Handlers.Seats
{
    public class GetSeatsBySectorIdHandler: IGetSeatsBySectorIdQueryHandler
    {
        private readonly ISeatRepository _repository;

        public GetSeatsBySectorIdHandler(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SeatResponse>> Handle(GetSeatsBySectorIdQuery query)
        {
            var seats = await _repository.GetSeatsBySectorId(query.SectorId);
            return seats.Select(s => new SeatResponse
            {
                
                SectorId = s.SectorId,
                RowIdentifier = s.RowIdentifier,
                SeatNumber = s.SeatNumber,
                Status = s.Status,
            }).ToList();
        }
    }
}
