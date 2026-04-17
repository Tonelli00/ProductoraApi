

namespace Application.Models.Responses
{
    public class SeatResponseDTO
    {
        public Guid SeatId { get; set; }
        public int SectorId { get; set; }
        public string RowIdentifier { get; set; }
        public int SeatNumber { get; set; } 
        public string Status { get; set; }

    }
}
