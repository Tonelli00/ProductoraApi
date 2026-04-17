

using Application.Models.Responses;

namespace Application.Interfaces
{
    public interface ISectorServices
    {
        Task<IEnumerable<SeatResponseDTO>> GetSeatsBySectorId(int SectorId);
    }
}
