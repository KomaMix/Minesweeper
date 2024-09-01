using Minesweeper.DTOs;
using Minesweeper.Repositories;

namespace Minesweeper.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

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
