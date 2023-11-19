using System.Text.Json;
using System.Text.Json.Serialization;

namespace GlobalCalc.Web.Converters;

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? str = reader.GetString();
        if (str == null || !DateTime.TryParse(str, out DateTime result))
            return DateTime.MinValue;

        return result;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss"));
    }
}
