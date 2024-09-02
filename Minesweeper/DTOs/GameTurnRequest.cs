using Newtonsoft.Json;

namespace Minesweeper.DTOs
{
    public class GameTurnRequest
    {
        [JsonProperty("game_id")]
        public Guid GameId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
