namespace Minesweeper.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Width { get; set; }
        public int Height { get; set; }
        public int MinesCount { get; set; }
        public bool IsCompleted { get; set; } = false;
        public char[,] Field { get; set; }
        public bool[,] Mines { get; set; }
    }
}
