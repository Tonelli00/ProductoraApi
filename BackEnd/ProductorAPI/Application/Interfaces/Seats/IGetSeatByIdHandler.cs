using Application.UseCase.Queries.Seats;
using Domain.Entities;

namespace Application.Interfaces.Seats
{
    public interface IGetSeatByIdHandler
    {
        Task<Seat?> Handle(GetSeatByIdQuery Query);
    }
}
