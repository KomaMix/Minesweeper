namespace Minesweeper.DTOs
{
    public class GameStateDto
    {
        public Guid GameId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int MinesCount { get; set; }
        public char[,] Field { get; set; }
        public bool IsCompleted { get; set; }
    }
}
