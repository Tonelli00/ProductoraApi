using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatServices _service;

        public SeatController(ISeatServices service)
        {
            _service = service;
        }

        [HttpGet("seats")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _service.GetAllSeats();
            return Ok(result);

        }
    }
}
