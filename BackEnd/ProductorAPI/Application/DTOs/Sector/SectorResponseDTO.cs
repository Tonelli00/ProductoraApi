using Application.DTOs.Seat;

namespace Application.DTOs;

public class SectorResponseDTO
{
    public int SectorId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public List<SeatResponseDTO> Seats { get; set; }
}