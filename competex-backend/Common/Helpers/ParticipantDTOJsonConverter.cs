using competex_backend.API.DTOs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace competex_backend.Common.Helpers
{
    public class ParticipantDTOJsonConverter : JsonConverter<ParticipantDTO>
    {
        public override ParticipantDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonDoc = JsonDocument.ParseValue(ref reader);
            if (jsonDoc.RootElement.TryGetProperty("customIndicator", out var customIndicator))
            {
                switch (customIndicator.GetString())
                {
                    case "Team":
                        return JsonSerializer.Deserialize<TeamDTO>(jsonDoc.RootElement.GetRawText(), options);
                    case "Single":
                        return JsonSerializer.Deserialize<SingleDTO>(jsonDoc.RootElement.GetRawText(), options);
                    default:
                        throw new JsonException("Unknown type");
                }
            }
            throw new JsonException("Missing discriminator");
        }


        public override void Write(Utf8JsonWriter writer, ParticipantDTO value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }

    }

}
