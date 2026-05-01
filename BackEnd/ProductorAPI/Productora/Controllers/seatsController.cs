using Application.DTOs;
using Application.DTOs.Reservation;
using Application.DTOs.Seat;
using Application.Interfaces.Seats;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Queries.Seats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly IGetSeatsBySectorIdHandler _getSeatsBySectorIdHandler;
        private readonly IMarkSeatAsReservedHandler _markSeatAsReserved;
        private readonly IGetReservedSeatsByEventHandler _getReservedSeatsByEventHandler;


        public SeatsController(IGetSeatsBySectorIdHandler handler, IMarkSeatAsReservedHandler markSeatAsReserved, IGetReservedSeatsByEventHandler getReservedSeatsByEventHandler)
        {
            _getSeatsBySectorIdHandler = handler;
            _markSeatAsReserved = markSeatAsReserved;
            _getReservedSeatsByEventHandler = getReservedSeatsByEventHandler;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SeatResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorReponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorReponseDTO), 404)]


        public async Task<IActionResult> GetAll([FromRoute] int id)
        {

            var result = await _getSeatsBySectorIdHandler.Handle(new GetSeatsBySectorIdQuery { SectorId=id});
            return Ok(result);

        }

        [HttpGet("reserved")]
        [ProducesResponseType(typeof(ReservationResponse), 200)]
        [ProducesResponseType(typeof(ErrorReponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorReponseDTO), 404)]

        public async Task<IActionResult> GetReserved([FromQuery] GetReservedSeatsByEventIdQuery query)
        {

            var result = await _getReservedSeatsByEventHandler.Handle(query);
            return Ok(result);

        }
        
    }
}
