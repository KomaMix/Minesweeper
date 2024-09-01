using Minesweeper.DTOs;
using Minesweeper.Models;
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
            var game = new Game
            {
                Width = width,
                Height = height,
                MinesCount = minesCount,
                Field = new char[width, height],
                Mines = new bool[width, height]
            };

            InitializeField(game);
            PlaceMines(game);

            _gameRepository.CreateGame(game);

            return MapToDto(game);
        }

        public GameStateDto MakeMove(Guid gameId, int row, int col)
        {
            // Если в репозитории нет игры, то выбросится исключение
            var game = _gameRepository.GetGame(gameId);

            if (game.IsCompleted)
                throw new InvalidOperationException("Игра уже завершена!");

            if (row < 0 || row >= game.Height || col < 0 || col >= game.Width)
                throw new ArgumentOutOfRangeException("Укажите существующую ячейку!");

            if (game.Field[row, col] != ' ')
                throw new InvalidOperationException("Ячейка уже открыта!");


            if (game.Mines[row, col])
            {
                // Игрок попал на мину, игра завершена
                RevealMines(game);
                game.IsCompleted = true;
            } else
            {
                int minesAround = CountMinesAround(game, row, col);

                game.Field[row, col] = minesAround.ToString()[0];

                if (minesAround == 0)
                {
                    RevealAdjacentCells(game, row, col);
                }
            }
        }

        private void InitializeField(Game game)
        {
            for (int i = 0; i < game.Width; i++)
            {
                for (int j = 0; j < game.Height; j++)
                {
                    game.Field[i, j] = ' ';
                }
            }
        }

        private void PlaceMines(Game game)
        {
            var random = new Random();
            int cntPlaced = 0;

            while (cntPlaced < game.MinesCount)
            {
                int row = random.Next(0, game.Width);
                int col = random.Next(0, game.Height);

                if (!game.Mines[row, col])
                {
                    game.Mines[row, col] = true;
                    cntPlaced++;
                }
            }
        }

        private GameStateDto MapToDto(Game game)
        {
            return new GameStateDto
            {
                GameId = game.Id,
                Width = game.Width,
                Height = game.Height,
                MinesCount = game.MinesCount,
                Field = game.Field,
                IsCompleted = game.IsCompleted
            };
        }

        private void RevealMines(Game game)
        {
            for (int i = 0; i < game.Width; i++)
            {
                for (int j = 0; j < game.Height; j++)
                {
                    if (game.Mines[i, j])
                    {
                        game.Field[i, j] = 'X';
                    }
                }
            }
        }

        private int CountMinesAround(Game game, int row, int col)
        {
            int cntMinesAround = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int new_row = row + i;
                    int new_col = col + j;

                    if (new_row > -1 && new_col > -1 
                        && new_row < game.Width && new_col < game.Height
                        && game.Mines[new_row, new_col])
                    {
                        cntMinesAround++;
                    }
                }
            }

            return cntMinesAround;
        }

        private void OpeningAdjacentCells(game, row, col)
        {

        }
    }
}
