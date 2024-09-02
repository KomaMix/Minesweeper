using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.DTOs;
using Minesweeper.Models;
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


        [HttpPost("new")]
        public ActionResult<GameStateDto> CreateGame([FromBody] CreateGameDto createDto)
        {
            if (createDto.Width <= 0 || createDto.Height <= 0 || createDto.MinesCount <= 0)
            {
                return BadRequest("Введены отрицательные параметры!");
            }

            if (createDto.Width > 30 || createDto.Height > 30 
                || createDto.MinesCount > createDto.Width * createDto.Height - 1)
            {
                return BadRequest("Параметры не соответсвуют правилам игры!");
            }

            var gameState = _gameService.CreateGame(createDto.Width, createDto.Height, createDto.MinesCount);
            return Ok(gameState);
        }
        
        [HttpGet("turn")]
        public ActionResult<GameStateDto> Turn(Guid gameId, [FromBody] GameMoveDto move)
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
