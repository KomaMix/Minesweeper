using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.DTOs;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpPost("new")]
        public ActionResult<GameInfoResponse> CreateGame([FromBody] NewGameRequest createDto)
        {
            if (createDto.Width < 2 || createDto.Width > 30)
            {
                return BadRequest(new { error = "ширина поля должна быть не менее 2 и не более 30" });
            }

            if (createDto.Height < 2 || createDto.Height > 30)
            {
                return BadRequest(new { error = "высота поля должна быть не менее 2 и не более 30" });
            }

            if (createDto.MinesCount < 1 || createDto.MinesCount > 24)
            {
                return BadRequest(new { error = "количество мин должно быть не менее 1 и не более 24" });
            }

            var gameState = _gameService.CreateGame(createDto.Width, createDto.Height, createDto.MinesCount);
            return Ok(gameState);
        }
        
        [HttpGet("turn")]
        public ActionResult<GameInfoResponse> Turn([FromBody] GameTurnRequest move)
        {
            try
            {
                var gameState = _gameService.MakeMove(gameId, move.Row, move.Col);
                return Ok(gameState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
