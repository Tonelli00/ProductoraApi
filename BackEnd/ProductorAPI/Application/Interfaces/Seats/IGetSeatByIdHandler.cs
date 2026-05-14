using Application.DTOs.Seat;
using Application.UseCase.Queries.Seats;
using Domain.Entities;

namespace Application.Interfaces.Seats
{
    public interface IGetSeatByIdHandler
    {
        Task<SeatResponseDTO?> Handle(GetSeatByIdQuery Query);
    }
}
