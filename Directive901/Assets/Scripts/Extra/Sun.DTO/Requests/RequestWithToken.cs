using Newtonsoft.Json;
using Sun.DTO.Helpers;
using System;

namespace Sun.DTO.Requests
{
    public class RequestWithToken : RequestWithoutToken
    {
        [JsonProperty("token")]
        [JsonConverter(typeof(GuidConverter))]
        public Guid Token { get; set; }
    }
}
