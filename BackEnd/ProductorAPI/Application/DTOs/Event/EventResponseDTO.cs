namespace Application.DTOs.Event;

public class EventResponseDTO
{
    public int Id { get; set; } 
    public string Name { get; set; } 
    public DateTime EventDate { get; set; } 
    public string Venue { get; set; } 
    public string Status { get; set; } 
    public List<SectorResponseDTO> Sectors { get; set; }
}