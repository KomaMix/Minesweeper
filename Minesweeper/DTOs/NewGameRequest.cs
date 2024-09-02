﻿using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Minesweeper.DTOs
{
    public class NewGameRequest
    {
        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        [JsonPropertyName("mines_count")]
        [JsonProperty("mines_count")]
        public int MinesCount { get; set; }
    }
}
