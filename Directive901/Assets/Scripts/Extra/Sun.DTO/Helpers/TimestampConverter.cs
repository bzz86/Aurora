using Newtonsoft.Json;
using System;

namespace Sun.DTO.Helpers
{
    public class TimestampConverter : JsonConverter
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = long.Parse((string)reader.Value);
            return epoch.AddSeconds(t);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            TimeSpan span = (DateTime)value - epoch;
            int unixTime = (int)span.TotalSeconds;
            writer.WriteRawValue(unixTime.ToString());
        }
    }
}
