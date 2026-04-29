using Application.Interfaces.Users;
using Application.UseCase.Commands.User;
using Application.UseCase.Queries.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserCommandHandler _commandHandler;
        private readonly IGetUserByIdQueryHandler _queryHandler;

        public UserController(IGetUserByIdQueryHandler queryHandler, ICreateUserCommandHandler commandHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _commandHandler.Handler(command);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var result = await _queryHandler.Handler(new GetUserByIdQuery { UserId = id});
            return Ok(result);
        }
    }
}
