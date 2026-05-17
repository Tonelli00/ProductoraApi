using Application.DTOs.Seat;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Documentation.SwaggerExamples.Seats
{
    public class SeatListExample : IExamplesProvider<IEnumerable<SeatResponseDTO>>
    {
        public IEnumerable<SeatResponseDTO> GetExamples()
        {
            return new List<SeatResponseDTO>
            {
                new SeatResponseDTO
                {
                    SeatId = Guid.NewGuid(),
                    RowIdentifier = "A",
                    SeatNumber = 1,
                    SectorId = 1,
                    Status = "Available"
                },
                new SeatResponseDTO
                {
                    SeatId = Guid.NewGuid(),
                    RowIdentifier = "A",
                    SeatNumber = 2,
                    SectorId = 1,
                    Status = "Reserved"
                },
                new SeatResponseDTO
                {
                    SeatId = Guid.NewGuid(),
                    RowIdentifier = "B",
                    SeatNumber = 1,
                    SectorId = 1,
                    Status = "Available"
                }
            };
        }
    }
        
}
