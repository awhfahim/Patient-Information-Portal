using Autofac;
using Microsoft.AspNetCore.Mvc;
using PatientPortal.Api.RequestHandlers;

namespace PatientPortal.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]s")]
    [ApiController]
    public class PatientController(ILogger<PatientController> logger, ILifetimeScope scope) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddPatientAsync([FromBody] PatientCreateRequestHandler patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            patient.Resolve(scope);
            
            var result = await patient.HandleAsync().ConfigureAwait(false);
            if (!result.IsError)
                return CreatedAtAction("GetPatients", "Patient", new { Id = result.Value }, result.Value);
            
            logger.LogError(result.FirstError.Description);
            return StatusCode(StatusCodes.Status500InternalServerError, result.FirstError.Description);
        }
        
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetPatientsAsync(Guid Id)
        {
            return Ok();
        }
    }
}
