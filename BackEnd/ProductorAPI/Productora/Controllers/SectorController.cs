using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorServices _service;

        public SectorController(ISectorServices service)
        {
            _service = service;
        }

        [HttpGet("{id:int}/seats")]
        public async Task<IActionResult> GetAllSeatsBySectorId(int id) 
        {
            var result = await _service.GetSeatsBySectorId(id);
            return Ok(result);
        }
    }
}
