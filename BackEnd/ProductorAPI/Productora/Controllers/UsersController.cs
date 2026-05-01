using Application.Interfaces.Users;
using Application.UseCase.Commands.User;
using Application.UseCase.Queries.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserCommandHandler _commandHandler;
        private readonly IGetUserByIdQueryHandler _queryHandler;
        private readonly ILoginUserHandler _loginUserHandler;


        public UsersController(IGetUserByIdQueryHandler queryHandler, ICreateUserCommandHandler commandHandler, ILoginUserHandler loginUserHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _loginUserHandler = loginUserHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _commandHandler.Handler(command);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _loginUserHandler.Handle(command);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var result = await _queryHandler.Handler(new GetUserByIdQuery { UserId = id});
            return Ok(result);
        }
    }
}
