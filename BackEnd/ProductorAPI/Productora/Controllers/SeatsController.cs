using Application.Interfaces;
using Application.UseCase.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly IGetSeatsBySectorIdHandler _getSeatsBySectorIdHandler;

        public SeatsController(IGetSeatsBySectorIdHandler handler)
        {
            _getSeatsBySectorIdHandler = handler;
        }

        [HttpGet("seats/{SectorId}")]
        public async Task<IActionResult> GetAll([FromRoute]GetSeatsBySectorIdQuery query)
        {

            var result = await _getSeatsBySectorIdHandler.Handle(query);
            return Ok(result);

        }
    }
}
