using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gnome.Application.Shared
{
    public class JObjectConverter : JsonConverter<JObject>
    {
        //
        // Summary:
        //     Compatiblity purposes Read method
        public override JObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JObject jObject = new JObject();
            string text = string.Empty;
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.PropertyName:
                        text = reader.GetString();
                        break;
                    case JsonTokenType.String:
                        jObject[text] = GetString(reader.GetString());
                        break;
                    case JsonTokenType.True:
                        jObject[text] = true;
                        break;
                    case JsonTokenType.False:
                        jObject[text] = false;
                        break;
                    case JsonTokenType.Null:
                        jObject[text] = null;
                        break;
                    case JsonTokenType.None:
                        jObject[text] = string.Empty;
                        break;
                    case JsonTokenType.Number:
                        jObject[text] = GetNumber(reader);
                        break;
                    case JsonTokenType.StartArray:
                        jObject[text] = new JArrayConverter().Read(ref reader, typeof(JArray), options, inArray: true);
                        break;
                    case JsonTokenType.StartObject:
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            jObject[text] = Read(ref reader, typeof(JObject), options);
                        }

                        break;
                    case JsonTokenType.EndObject:
                        return jObject;
                }
            }

            throw new NotSupportedException();
        }

        internal static JToken GetNumber(Utf8JsonReader reader)
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

        internal static JToken GetString(string @string)
        {
            return @string;
        }

        //
        // Summary:
        //     Compatiblity purposes Write method
        public override void Write(Utf8JsonWriter writer, JObject value, JsonSerializerOptions options)
        {
            Write(writer, value, options);
        }

        private void Write(Utf8JsonWriter writer, JObject @object, JsonSerializerOptions options, string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                writer.WriteStartObject();
            }
            else
            {
                writer.WriteStartObject(propertyName);
            }

            foreach (KeyValuePair<string, JToken> item in @object)
            {
                switch (item.Value.Type)
                {
                    case JTokenType.Integer:
                        WriteNumber(writer, item);
                        break;
                    case JTokenType.Float:
                        writer.WriteNumber(item.Key, item.Value.ToObject<float>());
                        break;
                    case JTokenType.Boolean:
                        writer.WriteBoolean(item.Key, item.Value.ToObject<bool>());
                        break;
                    case JTokenType.String:
                        writer.WriteString(item.Key, item.Value.ToString());
                        break;
                    case JTokenType.Date:
                        writer.WriteString(item.Key, item.Value.ToObject<DateTime>().ToString("yyyy-MM-dd HH:mm:ss"));
                        break;
                    case JTokenType.Null:
                        writer.WriteNull(item.Key);
                        break;
                    case JTokenType.TimeSpan:
                        writer.WriteString(item.Key, item.Value.ToString());
                        break;
                    case JTokenType.Guid:
                        writer.WriteString(item.Key, item.Value.ToString());
                        break;
                    case JTokenType.Object:
                        Write(writer, item.Value.ToObject<JObject>(), options, item.Key);
                        break;
                    case JTokenType.Array:
                        new JArrayConverter().Write(writer, item.Value.ToObject<JArray>(), options, item.Key);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            writer.WriteEndObject();
        }

        private static void WriteNumber(Utf8JsonWriter writer, KeyValuePair<string, JToken> item)
        {
            if (int.TryParse(item.Value.ToString(), out var result))
            {
                writer.WriteNumber(item.Key, result);
            }
            else
            {
                writer.WriteNumber(item.Key, item.Value.ToObject<long>());
            }
        }
    }
}