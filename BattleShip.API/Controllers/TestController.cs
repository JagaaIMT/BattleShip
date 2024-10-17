using Microsoft.AspNetCore.Mvc;

namespace BattleShip.API.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        // Route GET /test
        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok("Ceci est un test depuis un contrôleur");
        }
    }
}
