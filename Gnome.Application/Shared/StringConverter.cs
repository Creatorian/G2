using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public class StringConverter : JsonConverter<string>
    {
        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }


        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.True:
                    return "true";
                case JsonTokenType.False:
                    return "false";
                case JsonTokenType.Number:
                    {
                        string result;
                        if (!reader.TryGetInt64(out var value))
                        {
                            if (!reader.TryGetDouble(out var value2))
                            {
                                throw new JsonException();
                            }

                            result = Convert.ToString(value2);
                        }
                        else
                        {
                            result = Convert.ToString(value);
                        }
                        return result;
                    }
                case JsonTokenType.String:
                    return reader.GetString();
                default:
                    throw new JsonException();
            }
        }
    }
}
