using Newtonsoft.Json;

namespace Sun.DTO.Requests
{
    public class AuthorizationRequestDTO : RequestWithoutToken
    {
        public class Data
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }

        [JsonProperty("data")]
        public Data Payload { get; set; }
    }
}
