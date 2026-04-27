
namespace Application.DTOs.Seat
{
    public class SeatResponseDTO
    {
        public Guid SeatId { get; set; }
        public string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; }
    }
}
