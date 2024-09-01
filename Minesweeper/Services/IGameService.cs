using Minesweeper.DTOs;

namespace Minesweeper.Services
{
    public interface IGameService
    {
        GameStateDto CreateGame(int width, int height, int minesCount);
        GameStateDto MakeMove(Guid gameId, int row, int col);
    }
}
