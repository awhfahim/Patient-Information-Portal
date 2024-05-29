using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Api.Controllers
{
    [Route("api/[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class AllergieController(ILogger<AllergieController> logger, 
        IAllergieManagementService service) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var allergies = await service.GetAllergiesAsync(cancellationToken)
                .ConfigureAwait(false);
            
            if (allergies.IsError)
            {
                logger.Log(LogLevel.Error, allergies.FirstError.Code);
                return StatusCode(StatusCodes.Status500InternalServerError, allergies.FirstError.Code);
            }
            
            if(allergies.Value.IsNullOrEmpty())
            {
                return NoContent();
            }
            
            return Ok(allergies.Value);
        }
    }
    
}
