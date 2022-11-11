using Microsoft.AspNetCore.Mvc;

namespace SimTECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var stuff = new List<string>
            {
                "je",
                "suis",
                "stuff"
            };

            return new OkObjectResult(stuff);
        }
    }
}
