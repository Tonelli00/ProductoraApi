using Application.DTOs;
using Application.DTOs.Event;
using Application.DTOs.Seat;
using Application.Interfaces.Events;
using Application.UseCase.Queries.Events;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productora.Documentation.SwaggerExamples.Events;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly IGetPagedEventsHandler _getPagedEventsHandler;
        private readonly IGetSeatsByEventIdHandler _getSeatsByEventIdHandler;
        private readonly IGetEventByIdHandler _getEventByIdHandler;

        public EventsController(IGetPagedEventsHandler getPagedEventsHandler,
            IGetEventByIdHandler getEventByIdHandler,IGetSeatsByEventIdHandler getSeatsByEventIdHandler)
        {
            _getPagedEventsHandler = getPagedEventsHandler;
            _getEventByIdHandler = getEventByIdHandler;
            _getSeatsByEventIdHandler = getSeatsByEventIdHandler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EventShortResponseDTO), 200)]
        public async Task<IActionResult> GetPagedEvents([FromQuery]int Page=1, [FromQuery] int PageSize=10)
        {
            return Ok(await _getPagedEventsHandler.Handle(Page,PageSize));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(EventResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

        [SwaggerResponse(404, "Event not found", typeof(ErrorResponseDTO))]

        [SwaggerResponseExample(404, typeof(EventNotFoundExample))]

        public async Task<IActionResult> GetEventById(int id)
        {
            var result = await _getEventByIdHandler.Handle(new GetEventByIdQuery
            {
                EventId = id
            });
            return Ok(result);
        }
        [HttpGet("{id}/seats")]
        [ProducesResponseType(typeof(SeatResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

        public async Task<IActionResult> GetSeatsByEventId(int id)
        {
            var result = await _getSeatsByEventIdHandler.Handle(new GetSeatsByEventIdQuery
            {
                EventId = id
            });
            return Ok(result);
        }
    }
}
