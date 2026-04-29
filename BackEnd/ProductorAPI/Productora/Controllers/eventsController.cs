using Application.Interfaces.Events;
using Application.UseCase.Queries.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly IGetPagedEventsQueryHandler _getPagedEventsHandler;
        private readonly IGetSeatsByEventIdHandler _getSeatsByEventIdHandler;
        private readonly IGetEventByIdHandler _getEventByIdHandler;
        public EventsController(IGetPagedEventsQueryHandler getPagedEventsHandler,
            IGetEventByIdHandler getEventByIdHandler,IGetSeatsByEventIdHandler getSeatsByEventIdHandler)
        {
            _getPagedEventsHandler = getPagedEventsHandler;
            _getEventByIdHandler = getEventByIdHandler;
            _getSeatsByEventIdHandler = getSeatsByEventIdHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedEvents([FromQuery]int Page=1, [FromQuery] int PageSize=10)
        {
            return Ok(await _getPagedEventsHandler.Handle(Page,PageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var result = await _getEventByIdHandler.Handle(new GetEventByIdQuery
            {
                EventId = id
            });
            return Ok(result);
        }
        [HttpGet("{id}/seats")]
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
