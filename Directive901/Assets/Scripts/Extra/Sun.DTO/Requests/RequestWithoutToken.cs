using Newtonsoft.Json;

namespace Sun.DTO.Requests
{
    public abstract class RequestWithoutToken
    {
        [JsonProperty("command")]
        public string Command { get; set; }
    }
}
