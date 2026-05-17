using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces.Users;
using Application.UseCase.Commands.User;
using Application.UseCase.Queries.Users;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Productora.Documentation.SwaggerExamples.Errors;
using Productora.Documentation.SwaggerExamples.Users;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserCommandHandler _commandHandler;
        private readonly IGetUserByIdHandler _queryHandler;
        private readonly ILoginUserHandler _loginUserHandler;


        public UsersController(IGetUserByIdHandler queryHandler, ICreateUserCommandHandler commandHandler, ILoginUserHandler loginUserHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _loginUserHandler = loginUserHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 409)]

        [SwaggerResponse(201, "Created", typeof(UserResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(ErrorResponseDTO))]
        [SwaggerResponse(409, "Conflict", typeof(ErrorResponseDTO))]

        [SwaggerResponseExample(201, typeof(UserResponse))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(409, typeof(EmailConflictExample))]

        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _commandHandler.Handler(command);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 401)]

        [SwaggerResponse(200, "OK", typeof(UserResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(ErrorResponseDTO))]

        [SwaggerResponseExample(200, typeof(UserResponse))]
        [SwaggerResponseExample(401, typeof(UsercredentialsIncorrectExample))]

        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _loginUserHandler.Handle(command);
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

        [SwaggerResponse(200, "OK", typeof(UserResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(ErrorResponseDTO))]
        [SwaggerResponse(404, "Not Found", typeof(ErrorResponseDTO))]

        [SwaggerResponseExample(200, typeof(UserResponse))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(404, typeof(UserNotFoundExample))]

        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var result = await _queryHandler.Handler(new GetUserByIdQuery { UserId = id});
            return Ok(result);
        }
    }
}
