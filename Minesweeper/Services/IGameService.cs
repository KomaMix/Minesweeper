using Minesweeper.DTOs;

namespace Minesweeper.Services
{
    public interface IGameService
    {
        GameInfoResponse CreateGame(int width, int height, int minesCount);
        GameInfoResponse MakeMove(Guid gameId, int row, int col);
    }
}
