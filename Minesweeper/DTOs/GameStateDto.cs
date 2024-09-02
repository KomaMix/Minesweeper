using Newtonsoft.Json;

namespace Minesweeper.DTOs
{
    public class GameStateDto
    {
        public Guid GameId { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int MinesCount { get; set; }

        [JsonProperty("field")]
        public char[,] Field { get; set; }
        public bool Completed { get; set; }
    }
}
