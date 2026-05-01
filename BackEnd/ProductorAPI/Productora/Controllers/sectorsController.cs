using Application.DTOs;
using Application.DTOs.Reservation;
using Application.Interfaces.AuditLogs;
using Application.Interfaces.Sectors;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Commands.Sector;
using Application.UseCase.Queries.AuditLogs;
using Application.UseCase.Queries.Sectors;
using Microsoft.AspNetCore.Mvc;

namespace Productora.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class SectorsController : ControllerBase
{
    private readonly IGetSectorByIdHandler _getSectorByIdHandler;
    private readonly ICreateSectorHandler _createSectorHandler;

    public SectorsController(IGetSectorByIdHandler getSectorByIdHandler, ICreateSectorHandler createSectorHandler)
    {
        _getSectorByIdHandler = getSectorByIdHandler;
        _createSectorHandler = createSectorHandler;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SectorShortResponseDTO), 201)]
    [ProducesResponseType(typeof(ErrorReponseDTO), 400)]
    [ProducesResponseType(typeof(ErrorReponseDTO), 404)]
    [ProducesResponseType(typeof(ErrorReponseDTO), 409)]

    public async Task<IActionResult> CreateSector([FromBody]CreateSectorCommand command)
   {
      var result = await _createSectorHandler.Handle(command);
      return CreatedAtAction(nameof(GetSector), new { id = result.SectorId }, result);
   }

   [HttpGet("{id:int}")]
   [ProducesResponseType(typeof(SectorShortResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorReponseDTO), 400)]
    [ProducesResponseType(typeof(ErrorReponseDTO), 404)]
    [ProducesResponseType(typeof(ErrorReponseDTO), 409)]

    public async Task<IActionResult> GetSector(int id)
   {
      var result = await _getSectorByIdHandler.Handle(new GetSectorByIdQuery { SectorId = id });
      return Ok(result);
   }
}