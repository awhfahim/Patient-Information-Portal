using Microsoft.AspNetCore.Mvc;

namespace PatientPortal.Api.Controllers
{
    [Route("api/[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class AllergieController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllergies()
        {
            List<AllergieModel> allergies = [
                new AllergieModel{Id = Guid.NewGuid(), Name = "Dust Mite"}, 
                new AllergieModel{Id = Guid.NewGuid(), Name = "Pollen"},
            new AllergieModel(){Id = Guid.NewGuid(), Name = "Peanut"}
            ];
            return Ok(allergies);
        }
    }
    public class AllergieModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
