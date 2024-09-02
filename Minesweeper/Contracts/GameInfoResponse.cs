using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Minesweeper.DTOs
{
    public class GameInfoResponse
    {
        [Required]
        [JsonPropertyName("game_id")]
        [JsonProperty("game_id")]
        public Guid GameId { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        [JsonPropertyName("mines_count")]
        [JsonProperty("mines_count")]
        public int MinesCount { get; set; }

        [Required]
        [JsonPropertyName("field")]
        [JsonProperty("field")]
        public char[,] Field { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}
