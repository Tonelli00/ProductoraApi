using Application.Interfaces.Users;
using Application.UseCase.Queries.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IGetUserByIdQueryHandler _queryHandler;

        public userController(IGetUserByIdQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdQuery query)
        {
            var result = await _queryHandler.Handler(query);
            return Ok(result);
        }
    }
}
