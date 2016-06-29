using Newtonsoft.Json;
using Sun.DTO.Helpers;
using System;

namespace Sun.DTO.Responses
{
    public abstract class BaseResponseDTO
    {
        [JsonProperty("command")]
        public string Command { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("time")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }
}
