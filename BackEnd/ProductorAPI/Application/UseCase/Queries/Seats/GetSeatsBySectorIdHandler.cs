using Application.DTOs.Seat;
using Application.Interfaces.Seats;
using Application.Interfaces.Sectors;
using Application.UseCase.Queries.Sectors;
using Domain.Exceptions;


namespace Application.UseCase.Queries.Seats
{
    public class GetSeatsBySectorIdHandler: IGetSeatsBySectorIdHandler
    {
        private readonly ISeatRepository _repository;
        private readonly IGetSectorByIdHandler _getSectorByIdHandler;

        public GetSeatsBySectorIdHandler(ISeatRepository repository, IGetSectorByIdHandler getSectorByIdHandler)
        {
            _repository = repository;
            _getSectorByIdHandler = getSectorByIdHandler;  
        }

        public async Task<IEnumerable<SeatResponseDTO>> Handle(GetSeatsBySectorIdQuery query)
        {
            if(query.SectorId==0 || query.SectorId < 0) 
            {
                throw new ArgumentException("Ingrese valores válidos");
            }

            var sector = await _getSectorByIdHandler.Handle(new GetSectorByIdQuery { SectorId=query.SectorId});

            if (sector == null)
            {
                throw new SectorNotFoundException("El sector indicado no existe.");
            }
            
            var seats = await _repository.GetSeatsBySectorId(query.SectorId);
         

            return seats.Select(s => new SeatResponseDTO
            {
                SeatId = s.Id,
                RowIdentifier = s.RowIdentifier,
                SeatNumber = s.SeatNumber,
                SectorId=s.SectorId,
                Status = s.Status,
            }).ToList();
        }
    }
}
