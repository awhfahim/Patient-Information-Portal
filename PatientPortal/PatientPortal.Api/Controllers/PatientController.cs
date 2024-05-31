using Autofac;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatientPortal.Api.RequestHandlers;

namespace PatientPortal.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]s")]
    [ApiController, EnableCors("AllowAll")]
    public class PatientController(ILogger<PatientController> logger, ILifetimeScope scope) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddPatientAsync([FromBody] PatientCreateRequestHandler patient, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            patient.Resolve(scope);
            
            var result = await patient.HandleAsync(cancellationToken).ConfigureAwait(false);
            if (!result.IsError)
                return CreatedAtAction("GetById", "Patient", new { Id = result.Value }, result.Value);
            
            logger.LogError(result.FirstError.Description);
            return StatusCode(StatusCodes.Status500InternalServerError, result.FirstError.Code);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var handler = scope.Resolve<GetPatientRequestHandler>();
            var patients = await handler.GetPatientsAsync(cancellationToken);
            return patients.IsError ? StatusCode(StatusCodes.Status500InternalServerError, patients.FirstError.Code) 
                : Ok(patients.Value);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var handler = scope.Resolve<GetPatientRequestHandler>();
            var patient = await handler.GetPatientAsync(id, cancellationToken);
            return Ok();
        }
    }
}
