using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly bool useLocalDateTimeSerialization = (Environment.GetEnvironmentVariable("USE_LOCAL_DATETIME_SERIALIZATION") ?? "false").Equals("true");

        //
        // Summary:
        //     Writes DateTime value
        //
        // Parameters:
        //   writer:
        //
        //   value:
        //
        //   options:
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value.Kind == DateTimeKind.Unspecified)
            {
                value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            }

            if (useLocalDateTimeSerialization)
            {
                writer.WriteStringValue(value);
            }
            else
            {
                writer.WriteStringValue(value.ToUniversalTime());
            }
        }

        //
        // Summary:
        //     Read value from string
        //
        // Parameters:
        //   reader:
        //
        //   typeToConvert:
        //
        //   options:
        //
        // Exceptions:
        //   T:System.Text.Json.JsonException:
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (!DateTime.TryParse(reader.GetString(), out var result))
                {
                    throw new JsonException();
                }

                return result;
            }

            throw new JsonException();
        }
    }
}
