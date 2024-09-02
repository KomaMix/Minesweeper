using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Services;

namespace Minesweeper.Controllers
{
    [Route("api")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpGet("new")]
        public int CreateGame()
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
