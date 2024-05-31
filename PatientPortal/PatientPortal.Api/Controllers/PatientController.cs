using Autofac;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatientPortal.Api.RequestHandlers;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]s")]
    [ApiController, EnableCors("AllowAll")]
    public class PatientController(ILogger<PatientController> logger, ILifetimeScope scope) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientCreateRequestHandler patient, CancellationToken cancellationToken)
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
            return patient.IsError ? StatusCode(StatusCodes.Status500InternalServerError, patient.FirstError.Code) 
                : Ok(patient.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var service = scope.Resolve<IPatientManagementService>();
            var result = await service.DeletePatientAsync(id, cancellationToken);
            return result.IsError ? StatusCode(StatusCodes.Status500InternalServerError, result.Errors) 
                : Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PatientUpdateRequestHandler model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model.Resolve(scope);
            var result = await model.UpdatePatientAsync(cancellationToken);
            return result.IsError? StatusCode(StatusCodes.Status500InternalServerError, result.Errors) : Ok(result.Value);
        }
    }
}
