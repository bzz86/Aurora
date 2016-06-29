using Newtonsoft.Json;

namespace Sun.DTO.Responses
{
    public class ErrorResponseDTO : BaseResponseDTO
    {
        public class Data
        {
            [JsonProperty("error")]
            public int ErrorCode { get; set; }
        }

        [JsonProperty("data")]
        public Data Payload { get; set; }
    }
}
