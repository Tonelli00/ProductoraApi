
namespace Application.DTOs.Seat
{
    public class SeatResponse
    {
        public Guid SeatId { get; set; }
        public int SectorId { get; set; }
        public string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; }
    }
}
