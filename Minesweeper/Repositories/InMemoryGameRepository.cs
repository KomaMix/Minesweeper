using Minesweeper.Models;

namespace Minesweeper.Repositories
{
    // Репозиторий для хранения состояния игры
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly Dictionary<Guid, Game> _gamesStorage = new Dictionary<Guid, Game>();
        public void CreateGame(Game game)
        {
            if (_gamesStorage.ContainsKey(game.Id))
            {
                throw new InvalidOperationException("Игра с таким Id уже была создана!");
            }
            _gamesStorage.Add(game.Id, game);
        }

        public Game GetGame(Guid gameId)
        {
            if (!_gamesStorage.ContainsKey(gameId))
            {
                throw new InvalidOperationException("Нет игры с таким Id!");
            }
            return _gamesStorage[gameId];
        }

        public void UpdateGame(Game game)
        {
            if (!_gamesStorage.ContainsKey(game.Id))
            {
                throw new InvalidOperationException("Нет игры с таким Id!");
            }
            _gamesStorage[game.Id] = game;
        }
    }
}
