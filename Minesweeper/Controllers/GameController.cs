using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Minesweeper.Controllers
{
    [Route("api")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet("new")]
        public int New()
        {
            return 2;
        }

        [HttpGet("turn")]
        public int Turn()
        {
            return 3;
        }
    }
}
