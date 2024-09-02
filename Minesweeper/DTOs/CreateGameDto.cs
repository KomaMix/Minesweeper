using Newtonsoft.Json;

namespace Minesweeper.DTOs
{
    public class CreateGameDto
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int MinesCount { get; set; }
    }
}
