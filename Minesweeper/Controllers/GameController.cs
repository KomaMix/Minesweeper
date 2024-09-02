using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.DTOs;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(GameInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpPost("new")]
        public IActionResult CreateGame([FromBody] NewGameRequest createDto)
        {
            if (createDto.Width < 2 || createDto.Width > 30)
            {
                return BadRequest(new ErrorResponse { Error = "ширина поля должна быть не менее 2 и не более 30" });
            }

            if (createDto.Height < 2 || createDto.Height > 30)
            {
                return BadRequest(new ErrorResponse { Error = "высота поля должна быть не менее 2 и не более 30" });
            }

            if (createDto.MinesCount < 1 || createDto.MinesCount > 24)
            {
                return BadRequest(new ErrorResponse { Error = "количество мин должно быть не менее 1 и не более 24" });
            }

            var gameState = _gameService.CreateGame(createDto.Width, createDto.Height, createDto.MinesCount);
            return Ok(gameState);
        }
        
        [HttpPost("turn")]
        public IActionResult Turn([FromBody] GameTurnRequest move)
        {
            try
            {
                var gameState = _gameService.MakeMove(move.GameId, move.Row, move.Col);
                return Ok(gameState);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { Error = ex.Message });
            }
        }
    }
}
