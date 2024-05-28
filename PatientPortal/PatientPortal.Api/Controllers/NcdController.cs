using Microsoft.AspNetCore.Mvc;

namespace PatientPortal.Api.Controllers
{
    [Route("api/[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class NcdController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetNcds()
        {
            List<NcdInfoModel> ncds = [
                new NcdInfoModel{Id = Guid.NewGuid(), Name = "Cardiovascular"}, 
                new NcdInfoModel{Id = Guid.NewGuid(), Name = "diabetes"},
            new NcdInfoModel(){Id = Guid.NewGuid(), Name = "cancer"}
            ];
            return Ok(ncds);
        }
    }
    
    public class NcdInfoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
