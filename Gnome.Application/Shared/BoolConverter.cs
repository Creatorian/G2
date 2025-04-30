using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gnome.Application.Shared
{
    public class BoolConverter : JsonConverter<bool>
    {
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.String:
                    {
                        if (!bool.TryParse(reader.GetString(), out var result))
                        {
                            throw new JsonException();
                        }
                        return result;
                    }
                case JsonTokenType.Number:
                    {
                        long value;
                        double value2;
                        return reader.TryGetInt64(out value) ? Convert.ToBoolean(value) : (reader.TryGetDouble(out value2) && Convert.ToBoolean(value2));
                    }
                default:
                    throw new JsonException();
            }
        }
    }
}