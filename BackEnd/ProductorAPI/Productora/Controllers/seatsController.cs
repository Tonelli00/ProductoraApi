using Application.DTOs;
using Application.DTOs.Reservation;
using Application.DTOs.Seat;
using Application.Interfaces.Seats;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Queries.Seats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productora.Documentation.SwaggerExamples.Seats;
using Productora.Documentation.SwaggerExamples.Sectors;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly IGetSeatsBySectorIdHandler _getSeatsBySectorIdHandler;
        private readonly IMarkSeatAsReservedHandler _markSeatAsReserved;
        private readonly IGetReservedSeatsByEventHandler _getReservedSeatsByEventHandler;
        private readonly IGetSeatByIdHandler _getSeatByIdHandler;


        public SeatsController(IGetSeatsBySectorIdHandler handler,IGetSeatByIdHandler getSeatByIdHandler, IMarkSeatAsReservedHandler markSeatAsReserved, IGetReservedSeatsByEventHandler getReservedSeatsByEventHandler)
        {
            _getSeatsBySectorIdHandler = handler;
            _markSeatAsReserved = markSeatAsReserved;
            _getReservedSeatsByEventHandler = getReservedSeatsByEventHandler;
            _getSeatByIdHandler = getSeatByIdHandler;
        }

        [HttpGet("{SectorId:int}")]
        [ProducesResponseType(typeof(List<SeatResponseDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]


        public async Task<IActionResult> GetAll([FromRoute] int SectorId)
        {

            var result = await _getSeatsBySectorIdHandler.Handle(new GetSeatsBySectorIdQuery { SectorId= SectorId });
            return Ok(result);

        }


        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(SeatResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]


        public async Task<IActionResult> GetSeatById([FromRoute] Guid id)
        {

            var result = await _getSeatByIdHandler.Handle(new GetSeatByIdQuery{ SeatId=id});
            return Ok(result);

        }

        [HttpGet("reserved")]
        [ProducesResponseType(typeof(ReservationResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

        [SwaggerResponse(404, "Not Found", typeof(ErrorResponseDTO))]

        [SwaggerResponseExample(404, typeof(SeatNotFoundExample))]

        public async Task<IActionResult> GetReserved([FromQuery] GetReservedSeatsByEventIdQuery query)
        {

            var result = await _getReservedSeatsByEventHandler.Handle(query);
            return Ok(result);

        }
        
    }
}
