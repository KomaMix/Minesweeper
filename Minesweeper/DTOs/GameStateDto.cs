using Newtonsoft.Json;

namespace Minesweeper.DTOs
{
    public class GameStateDto
    {
        [JsonProperty("game_id")]
        public Guid GameId { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        [JsonProperty("mines_count")]
        public int MinesCount { get; set; }

        [JsonProperty("field")]
        public char[,] Field { get; set; }
        public bool Completed { get; set; }
    }
}
