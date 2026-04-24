using Application.DTOs.Seat;
using Application.Interfaces.Seats;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Queries.Seats
{
    public class GetSeatByIdHandler : IGetSeatByIdHandler
    {
        private readonly ISeatRepository _seatRepository;

        public GetSeatByIdHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<Seat?> Handle(GetSeatByIdQuery Query)
        {
            var seat = await _seatRepository.GetSeatById(Query.SeatId);
            return seat;
        }
    }
}
