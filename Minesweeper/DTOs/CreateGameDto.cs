using Newtonsoft.Json;

namespace Minesweeper.DTOs
{
    public class CreateGameDto
    {
        public int Width { get; set; }
        public int Height { get; set; }

        [JsonProperty("mines_count")]
        public int MinesCount { get; set; }
    }
}
