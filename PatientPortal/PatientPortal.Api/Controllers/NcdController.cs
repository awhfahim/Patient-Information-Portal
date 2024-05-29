using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Api.Controllers
{
    [Route("api/[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class NcdController(ILogger<NcdController> logger, INcdManagementService ncdManagementService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var ncds = await ncdManagementService.GetNcdsAsync(cancellationToken)
                .ConfigureAwait(false);
            
            if (ncds.IsError)
            {
                logger.Log(LogLevel.Error ,ncds.FirstError.Code);
                return StatusCode(StatusCodes.Status500InternalServerError, ncds.FirstError.Code);
            }
            
            if(ncds.Value.IsNullOrEmpty())
            {
                return NoContent();
            }
            
            return Ok(ncds.Value);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetNcdById(Guid id, CancellationToken cancellationToken)
        {
            var ncd = await ncdManagementService.GetNcdByIdAsync(id, cancellationToken)
                .ConfigureAwait(false);
            
            if (ncd.IsError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ncd.FirstError.Code);
            }
            
            if(ncd.Value is null)
            {
                return NotFound();
            }
            
            return Ok(ncd.Value);
        }
    }
}
