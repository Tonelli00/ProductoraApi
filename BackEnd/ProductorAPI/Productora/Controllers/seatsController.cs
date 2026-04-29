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
        private readonly IGetSeatsBySectorIdQueryHandler _getSeatsBySectorIdHandler;
        private readonly IMarkSeatAsReservedCommandHandler _markSeatAsReservedCommand;
        private readonly IGetReservedSeatsByEventHandler _getReservedSeatsByEventHandler;

        public SeatsController(IGetSeatsBySectorIdQueryHandler handler, IMarkSeatAsReservedCommandHandler markSeatAsReservedCommand, IGetReservedSeatsByEventHandler getReservedSeatsByEventHandler)
        {
            _getSeatsBySectorIdHandler = handler;
            _markSeatAsReservedCommand = markSeatAsReservedCommand;
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
