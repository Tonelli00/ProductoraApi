using Application.Interfaces.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IGetAllEventsQueryHandler _getAllEventsHandler;
        private readonly IGetPagedEventsQueryHandler _getPagedEventsHandler;
        public EventsController(IGetAllEventsQueryHandler getAllEventsHandler,IGetPagedEventsQueryHandler getPagedEventsHandler)
        {
            _getAllEventsHandler = getAllEventsHandler;
            _getPagedEventsHandler = getPagedEventsHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedEvents([FromQuery]int Page=1, [FromQuery] int PageSize=10)
        {
            return Ok(await _getPagedEventsHandler.Handle(Page,PageSize));
        }
    }
}
