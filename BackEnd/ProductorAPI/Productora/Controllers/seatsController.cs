using Application.Interfaces.Seats;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Queries.Seats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class seatsController : ControllerBase
    {
        private readonly IGetSeatsBySectorIdHandler _getSeatsBySectorIdHandler;
        private readonly IMarkSeatAsReservedHandler _markSeatAsReserved;
        private readonly IGetReservedSeatsByEventHandler _getReservedSeatsByEventHandler;


        public seatsController(IGetSeatsBySectorIdHandler handler, IMarkSeatAsReservedHandler markSeatAsReserved, IGetReservedSeatsByEventHandler getReservedSeatsByEventHandler)
        {
            _getSeatsBySectorIdHandler = handler;
            _markSeatAsReserved = markSeatAsReserved;
            _getReservedSeatsByEventHandler = getReservedSeatsByEventHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetSeatsBySectorIdQuery query)
        {

            var result = await _getSeatsBySectorIdHandler.Handle(query);
            return Ok(result);

        }

        [HttpGet("Reserved")]
        public async Task<IActionResult> GetReserved([FromQuery] GetReservedSeatsByEventIdQuery query)
        {

            var result = await _getReservedSeatsByEventHandler.Handle(query);
            return Ok(result);

        }
        
    }
}
