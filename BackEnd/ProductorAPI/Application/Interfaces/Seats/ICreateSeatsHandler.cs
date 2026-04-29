using Application.UseCase.Commands.Seat;
using Domain.Entities;

namespace Application.Interfaces.Seats;

public interface ICreateSeatsHandler
{
    Task Handle(CreateSeatsCommand command);
}