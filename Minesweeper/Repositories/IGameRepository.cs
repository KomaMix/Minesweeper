using Minesweeper.Models;

namespace Minesweeper.Repositories
{
    public interface IGameRepository
    {
        void CreateGame(Game game);
        Game GetGame(Game game);
        void UpdateGame(Game game);
    }
}
