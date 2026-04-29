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
public class sectorsController : ControllerBase
{
   private readonly IGetSectorByIdHandler _getSectorByIdHandler;
   private readonly ICreateSectorHandler _createSectorHandler;

   public sectorsController(IGetSectorByIdHandler getSectorByIdHandler, ICreateSectorHandler createSectorHandler)
   {
      _getSectorByIdHandler = getSectorByIdHandler;
      _createSectorHandler = createSectorHandler;
   }

   [HttpPost]
   public async Task<IActionResult> CreateSector([FromBody]CreateSectorCommand command)
   {
      var result = await _createSectorHandler.Handle(command);
      return CreatedAtAction(nameof(GetSector), new { id = result.SectorId }, result);
   }

   [HttpGet("{id:int}")]
   public async Task<IActionResult> GetSector(int id)
   {
      var result = await _getSectorByIdHandler.Handle(new GetSectorByIdQuery { SectorId = id });
      return Ok(result);
   }
}