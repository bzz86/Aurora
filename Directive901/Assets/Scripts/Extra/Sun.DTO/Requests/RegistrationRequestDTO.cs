using Newtonsoft.Json;

namespace Sun.DTO.Requests
{
    public class RegistrationRequestDTO : RequestWithoutToken
    {
        public class Data
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("password")]
            public string Password { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
        }

        [JsonProperty("data")]
        public Data Payload { get; set; }
    }
}
