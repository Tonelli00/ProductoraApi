namespace Application.DTOs;

public class SectorShortResponseDTO
{
    public int SectorId { get; set; }
    public int EventId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
}