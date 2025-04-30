using Newtonsoft.Json.Linq;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace Gnome.Application.Shared
{
    public class JValueConverter : JsonConverter<JValue>
    {
        //
        // Summary:
        //     Compatiblity purposes Read method
        public override JValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JValue result = null;
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.String:
                        result = new JValue(reader.GetString());
                        break;
                    case JsonTokenType.True:
                        new JValue(value: true);
                        break;
                    case JsonTokenType.False:
                        new JValue(value: false);
                        break;
                    case JsonTokenType.Null:
                        JValue.CreateNull();
                        break;
                    case JsonTokenType.None:
                        new JValue(string.Empty);
                        break;
                    case JsonTokenType.Number:
                        new JValue(GetNumber(reader));
                        break;
                    default:
                        throw new NotSupportedException();
                    case JsonTokenType.Comment:
                        break;
                }
            }

            return result;
        }

        //
        // Summary:
        //     Compatiblity purposes Write method
        public override void Write(Utf8JsonWriter writer, JValue value, JsonSerializerOptions options)
        {
            Write(writer, value, options);
        }

        private void Write(Utf8JsonWriter writer, JValue @object, JsonSerializerOptions options, string propertyName = null)
        {
            switch (@object.Type)
            {
                case JTokenType.Integer:
                    writer.WriteNumberValue(@object.Value<int>());
                    break;
                case JTokenType.Float:
                    writer.WriteNumberValue(@object.Value<float>());
                    break;
                case JTokenType.Boolean:
                    writer.WriteBooleanValue(@object.Value<bool>());
                    break;
                case JTokenType.String:
                    writer.WriteStringValue(@object.Value.ToString());
                    break;
                case JTokenType.Date:
                    writer.WriteStringValue(@object.Value<DateTime>().ToString("yyyy-MM-dd HH:mm:ss"));
                    break;
                case JTokenType.Null:
                    writer.WriteNullValue();
                    break;
                case JTokenType.TimeSpan:
                    writer.WriteStringValue(@object.Value.ToString());
                    break;
                case JTokenType.Guid:
                    writer.WriteStringValue(@object.Value.ToString());
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private static JToken GetNumber(Utf8JsonReader reader)
        {
            if (reader.TryGetDecimal(out var value))
            {
                if (value == (decimal)(int)value)
                {
                    return (int)value;
                }

                return value;
            }

            if (reader.TryGetInt64(out var value2))
            {
                return value2.ToString();
            }

            throw new NotSupportedException();
        }
    }
}