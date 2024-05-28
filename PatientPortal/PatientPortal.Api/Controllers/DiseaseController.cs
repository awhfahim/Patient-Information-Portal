using Microsoft.AspNetCore.Mvc;

namespace PatientPortal.Api.Controllers
{
    [Route("api/[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDiseases()
        {
            List<DiseaseInfoModel> diseases = [
                new DiseaseInfoModel{Id = Guid.NewGuid(), Name = "asma"}, 
                new DiseaseInfoModel{Id = Guid.NewGuid(), Name = "diabetes"},
            new DiseaseInfoModel(){Id = Guid.NewGuid(), Name = "cancer"}
            ];
            return Ok(diseases);
        }
    }
    
    public class DiseaseInfoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
