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

        public GameInfoResponse CreateGame(int width, int height, int minesCount)
        {
            var game = new Game
            {
                Width = width,
                Height = height,
                MinesCount = minesCount,
                Field = new char[width, height],
                Mines = new bool[width, height]
            };

            // Начальное заполнение поля пустыми символами
            InitializeField(game);

            // Рандомное расположение мин
            PlaceMines(game);

            // Сохранение конфигурации игры в репозитории
            _gameRepository.CreateGame(game);

            // Преобразование объекта Game для отправки к клиенту
            return MapToDto(game);
        }

        public GameInfoResponse MakeMove(Guid gameId, int row, int col)
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

                // Нужно открыть все оставшиеся мины и клетки
                LoseRevealRemainingCels(game);

                game.IsCompleted = true;
            } else
            {
                // Количество мин рядом с этой клеткой
                int minesAround = CountMinesAround(game, row, col);

                // Присваиваем текущей клетке количество мин
                game.Field[row, col] = minesAround.ToString()[0];

                // Открываем все клетки рядом с нулевой
                if (minesAround == 0)
                {
                    OpeningAdjacentCells(game, row, col);
                }

                // Проверка выигрыша
                if (CheckWinCondition(game))
                {
                    game.IsCompleted = true;

                    // При выигрыше тоже требуется открыть все клетки, но немного в другом формате
                    WinRevealRemainingCels(game);
                }
            }

            // Обновляем состояние игры в репозитории
            _gameRepository.UpdateGame(game);

            // Возвращаем состояние игры
            return MapToDto(game);
        }

        // Начальная инициализация карты пустыми символами
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

        // Установка мин
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

        // Преобразование состояния игры
        // Расположение мин ни в коем случае нельзя раскрывать
        // Поэтому мы не можем возвратить объект Game
        private GameInfoResponse MapToDto(Game game)
        {
            return new GameInfoResponse
            {
                GameId = game.Id,
                Width = game.Width,
                Height = game.Height,
                MinesCount = game.MinesCount,
                Field = game.Field,
                Completed = game.IsCompleted
            };
        }

        // Количество мин рядом с этой клеткой
        private int CountMinesAround(Game game, int row, int col)
        {
            int cntMinesAround = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow != row || newCol != col)
                    {
                        if (newRow > -1 && newCol > -1
                        && newRow < game.Width && newCol < game.Height
                        && game.Mines[newRow, newCol])
                        {
                            cntMinesAround++;
                        }
                    }
                    
                }
            }

            return cntMinesAround;
        }

        // Открываем все клетки рядом с нулевой
        private void OpeningAdjacentCells(Game game, int row, int col)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow != row || newCol != col)
                    {
                        if (newRow >= 0 && newRow < game.Height && newCol >= 0 && newCol < game.Width && game.Field[newRow, newCol] == ' ')
                        {
                            MakeMove(game.Id, newRow, newCol);
                        }
                    }
                    
                }
            }
        }

        // Проверка выигрыша
        private bool CheckWinCondition(Game game)
        {
            for (int i = 0; i < game.Height; i++)
            {
                for (int j = 0; j < game.Width; j++)
                {
                    if (game.Field[i, j] == ' ' && !game.Mines[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Преобразуем карту на случай выигрыша
        private void WinRevealRemainingCels(Game game)
        {
            for (int i = 0; i < game.Height; i++)
            {
                for (int j = 0; j < game.Width; j++)
                {
                    if (game.Mines[i, j] && game.Field[i, j] == ' ')
                    {
                        game.Field[i, j] = 'M';
                    }
                }
            }
        }

        // Преобразуем карту на случай проигрыша
        private void LoseRevealRemainingCels(Game game)
        {
            for (int i = 0; i < game.Height; i++)
            {
                for (int j = 0; j < game.Width; j++)
                {
                    if (game.Mines[i, j])
                    {
                        game.Field[i, j] = 'X';
                    } else if (game.Field[i, j] == ' ')
                    {
                        int minesAround = CountMinesAround(game, i, j);

                        game.Field[i, j] = minesAround.ToString()[0];
                    }
                    
                }
            }
        }


    }
}
