using Application.Interfaces.Reservations;
using Application.UseCase.Commands.Reservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class reservationController : ControllerBase
    {
        private readonly ICreateReservationCommandHandler _createReservationHandler;

        public reservationController(ICreateReservationCommandHandler createReservationHandler)
        {
            _createReservationHandler = createReservationHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
        {
            var result = await _createReservationHandler.Handle(command);
            return Ok(result);
        }
    }
}
