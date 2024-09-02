using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Minesweeper.DTOs
{
    public class GameTurnRequest
    {
        [Required]
        [JsonPropertyName("game_id")]
        [JsonProperty("game_id")]
        public Guid GameId { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Col { get; set; }
    }
}
