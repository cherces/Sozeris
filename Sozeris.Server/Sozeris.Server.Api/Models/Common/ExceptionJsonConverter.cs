using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sozeris.Server.Api.Models.Common;

public class ExceptionJsonConverter : JsonConverter<Exception>
{
    public override Exception? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => throw new NotSupportedException();

    public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("title", value.Message);
        writer.WriteNumber("status", value is ValidationException ? 400 : 500);

        if (value is ValidationException vex)
        {
            writer.WritePropertyName("errors");
            JsonSerializer.Serialize(writer, vex.Errors, options);
        }

        writer.WriteEndObject();
    }
}