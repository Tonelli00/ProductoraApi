

using Application.Models.Responses;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISeatServices
    {
        Task<IEnumerable<SeatResponseDTO>> GetAllSeats();
    }
}
