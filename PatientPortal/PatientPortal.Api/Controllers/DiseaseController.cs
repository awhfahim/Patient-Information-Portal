using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Api.Controllers
{
    [Route("api/[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class DiseaseController(ILogger<DiseaseController> logger, 
        IDiseaseManagementService diseaseManagementService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var diseases = await diseaseManagementService.GetDiseasesAsync(cancellationToken)
                .ConfigureAwait(false);
            
            if (diseases.IsError)
            {
                logger.Log(LogLevel.Error ,diseases.FirstError.Code);
                return StatusCode(StatusCodes.Status500InternalServerError, diseases.FirstError.Code);
            }
            
            if(diseases.Value.IsNullOrEmpty())
            {
                return NoContent();
            }
            
            return Ok(diseases.Value);
        }
    }
    

}
