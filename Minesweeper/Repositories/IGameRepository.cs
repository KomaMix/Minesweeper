using Minesweeper.Models;

namespace Minesweeper.Repositories
{
    public interface IGameRepository
    {
        void CreateGame(Game game);
        Game GetGame(Guid gameId);
        void UpdateGame(Game game);
    }
}
