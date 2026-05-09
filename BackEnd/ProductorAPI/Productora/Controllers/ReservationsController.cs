using Application.DTOs;
using Application.DTOs.Reservation;
using Application.DTOs.Seat;
using Application.Interfaces.Reservations;
using Application.UseCase.Commands.Reservation;
using Application.UseCase.Queries.Reservations;
using Microsoft.AspNetCore.Mvc;
using Productora.Documentation.SwaggerExamples.Reservations;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ICreateReservationCommandHandler _createReservationHandler;
        private readonly IGetReservationsByUserQueryHandler _getReservationsByUserId;
        private readonly IGetReservationByIdQueryHandler _getReservationsById;

        public ReservationsController(ICreateReservationCommandHandler createReservationHandler, IGetReservationsByUserQueryHandler getReservationsByUserId,
            IGetReservationByIdQueryHandler getReservationsById)
        {
            _createReservationHandler = createReservationHandler;
            _getReservationsByUserId = getReservationsByUserId;
            _getReservationsById = getReservationsById;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReservationResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 409)]

        [SwaggerResponse(201, "Created", typeof(ReservationResponse))]

        [SwaggerResponseExample(201, typeof(ReservedSeatExample))]

        public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
        {
            var result = await _createReservationHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id:int}/user")]
        [ProducesResponseType(typeof(ReservationResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

        public async Task<IActionResult> GetByUser([FromRoute] int id)
        {
            var result = await _getReservationsByUserId.Handler(new GetReservationsByUserIDQuery { userId = id});
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ReservationResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

        [SwaggerResponse(404, "Not Found", typeof(ErrorResponseDTO))]

        [SwaggerResponseExample(404, typeof(ReservationNotFoundExample))]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _getReservationsById.Handle(new GetReservationByIdQueryHandler { reservationId = id });
            return Ok(result);
        }
    }
}
