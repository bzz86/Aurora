using Newtonsoft.Json;
using System;

namespace Sun.DTO.Responses
{
    public class AuthorizationResponseDTO : BaseResponseDTO
    {
        public class Data
        {
            [JsonProperty("token")]
            public Guid Token { get; set; }
        }

        [JsonProperty("data")]
        public Data Payload { get; set; }
    }
}
