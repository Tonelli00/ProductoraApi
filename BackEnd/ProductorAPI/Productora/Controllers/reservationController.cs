using Application.Interfaces.Reservations;
using Application.UseCase.Commands.Reservation;
using Application.UseCase.Queries.Reservations;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ICreateReservationCommandHandler _createReservationHandler;
        private readonly IGetReservationsByUserQueryHandler _getReservationsByUserId;
        private readonly IGetReservationByIdQueryHandler _getReservationsById;

        public ReservationController(ICreateReservationCommandHandler createReservationHandler, IGetReservationsByUserQueryHandler getReservationsByUserId,
            IGetReservationByIdQueryHandler getReservationsById)
        {
            _createReservationHandler = createReservationHandler;
            _getReservationsByUserId = getReservationsByUserId;
            _getReservationsById = getReservationsById;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
        {
            var result = await _createReservationHandler.Handle(command);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser([FromQuery] int userId)
        {
            var result = await _getReservationsByUserId.Handler(new GetReservationsByUserIDQuery { userId = userId});
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var result = await _getReservationsById.Handle(new GetReservationByIdQueryHandler { reservationId = id });
            return Ok(result);
        }
    }
}
