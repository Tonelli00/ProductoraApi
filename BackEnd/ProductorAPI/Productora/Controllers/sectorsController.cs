using Application.DTOs;
using Application.DTOs.Reservation;
using Application.Interfaces.AuditLogs;
using Application.Interfaces.Sectors;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Commands.Sector;
using Application.UseCase.Queries.AuditLogs;
using Application.UseCase.Queries.Sectors;
using Microsoft.AspNetCore.Mvc;
using Productora.Documentation.SwaggerExamples.Errors;
using Productora.Documentation.SwaggerExamples.Events;
using Productora.Documentation.SwaggerExamples.Reservations;
using Productora.Documentation.SwaggerExamples.Seats;
using Productora.Documentation.SwaggerExamples.Sectors;
using Productora.Documentation.SwaggerExamples.Users;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Productora.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class SectorsController : ControllerBase
{
    private readonly IGetSectorByIdHandler _getSectorByIdHandler;
    private readonly ICreateSectorHandler _createSectorHandler;
    private readonly IGetSectorSummaryHandler _getSectorSummary;

    public SectorsController(IGetSectorByIdHandler getSectorByIdHandler, ICreateSectorHandler createSectorHandler, IGetSectorSummaryHandler getSectorSummary)
    {
        _getSectorByIdHandler = getSectorByIdHandler;
        _createSectorHandler = createSectorHandler;
        _getSectorSummary = getSectorSummary;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SectorShortResponseDTO), 201)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 409)]

    [SwaggerResponse(201, "Created", typeof(SectorResponseDTO))]
    [SwaggerResponse(400, "Bad Request", typeof(ErrorResponseDTO))]
    [SwaggerResponse(404, "Not Found", typeof(ErrorResponseDTO))]
    [SwaggerResponse(409, "Conflict", typeof(ErrorResponseDTO))]

    [SwaggerResponseExample(201, typeof(SectorResponseDTO))]
    [SwaggerResponseExample(400, typeof(BadRequestExample))]
    [SwaggerResponseExample(404, typeof(EventNotFoundExample))]
    [SwaggerResponseExample(409, typeof(SectorConflictExample))]

    public async Task<IActionResult> CreateSector([FromBody]CreateSectorCommand command)
   {
      var result = await _createSectorHandler.Handle(command);
      return CreatedAtAction(nameof(GetSector), new { id = result.SectorId }, result);
   }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SectorResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

    [SwaggerResponse(200, "OK", typeof(SectorResponseDTO))]
    [SwaggerResponse(400, "Bad request", typeof(ErrorResponseDTO))]
    [SwaggerResponse(404, "Not Found", typeof(ErrorResponseDTO))]

    [SwaggerResponseExample(200, typeof(SectorResponseDTO))]
    [SwaggerResponseExample(400, typeof(BadRequestExample))]
    [SwaggerResponseExample(404, typeof(SectorNotFoundExample))]

    public async Task<IActionResult> GetSector(int id)
   {
      var result = await _getSectorByIdHandler.Handle(new GetSectorByIdQuery { SectorId = id });
      return Ok(result);
   }


    [HttpGet("{id:int}/summary")]
    [ProducesResponseType(typeof(SectorShortResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 404)]

    [SwaggerResponse(200, "OK", typeof(SectorShortResponseDTO))]
    [SwaggerResponse(400, "Bad request", typeof(ErrorResponseDTO))]
    [SwaggerResponse(404, "Not Found", typeof(ErrorResponseDTO))]
    
    [SwaggerResponseExample(200, typeof(SectorShortResponseDTO))]
    [SwaggerResponseExample(400, typeof(BadRequestExample))]
    [SwaggerResponseExample(404, typeof(SectorNotFoundExample))]

    public async Task<IActionResult> GetSectorSummary(int id)
    {
        var result = await _getSectorSummary.Handle(new GetSectorSummaryQuery{ SectorId = id });
        return Ok(result);
    }
}