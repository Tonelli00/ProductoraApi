using Application.DTOs.Seat;
using Application.Interfaces.Seats;


namespace Application.UseCase.Queries.Seats
{
    public class GetSeatsBySectorIdHandler: IGetSeatsBySectorIdQueryHandler
    {
        private readonly ISeatRepository _repository;

        public GetSeatsBySectorIdHandler(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SeatResponseDTO>> Handle(GetSeatsBySectorIdQuery query)
        {
            var seats = await _repository.GetSeatsBySectorId(query.SectorId);
            return seats.Select(s => new SeatResponseDTO
            {
                SeatId = s.Id,
                RowIdentifier = s.RowIdentifier,
                SeatNumber = s.SeatNumber,
                Status = s.Status,
            }).ToList();
        }
    }
}
