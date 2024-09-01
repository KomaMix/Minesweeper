using Minesweeper.DTOs;

namespace Minesweeper.Services
{
    public class GameService : IGameService
    {
        public GameStateDto CreateGame(int width, int height, int minesCount)
        {
            throw new NotImplementedException();
        }

        public GameStateDto MakeMove(Guid gameId, int row, int col)
        {
            throw new NotImplementedException();
        }
    }
}
