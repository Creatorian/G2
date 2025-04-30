using Newtonsoft.Json.Linq;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace Gnome.Application.Shared
{
    public class JArrayConverter : JsonConverter<JArray>
    {
        //
        // Parameters:
        //   reader:
        //
        //   typeToConvert:
        //
        //   options:
        public override JArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Read(ref reader, typeToConvert, options);
        }

        //
        // Summary:
        //     Compatiblity purposes Read method
        public JArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, bool inArray = false)
        {
            JArray jArray = new JArray();
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.String:
                        jArray.Add(JObjectConverter.GetString(reader.GetString()));
                        break;
                    case JsonTokenType.True:
                        jArray.Add(true);
                        break;
                    case JsonTokenType.False:
                        jArray.Add(false);
                        break;
                    case JsonTokenType.Null:
                        jArray.Add(null);
                        break;
                    case JsonTokenType.None:
                        jArray.Add(string.Empty);
                        break;
                    case JsonTokenType.Number:
                        jArray.Add(JObjectConverter.GetNumber(reader));
                        break;
                    case JsonTokenType.StartObject:
                        jArray.Add(new JObjectConverter().Read(ref reader, typeof(JArray), options));
                        break;
                    case JsonTokenType.StartArray:
                        if (inArray)
                        {
                            jArray.Add(Read(ref reader, typeof(JArray), options));
                        }

                        inArray = true;
                        break;
                    case JsonTokenType.EndArray:
                        return jArray;
                    case JsonTokenType.PropertyName:
                        throw new NotSupportedException();
                }
            }

            throw new NotSupportedException();
        }

        //
        // Summary:
        //     Compatiblity purposes Write method
        public override void Write(Utf8JsonWriter writer, JArray value, JsonSerializerOptions options)
        {
            Write(writer, value, options);
        }

        //
        // Summary:
        //     Compatiblity purposes Write method
        public void Write(Utf8JsonWriter writer, JArray array, JsonSerializerOptions options, string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                writer.WriteStartArray();
            }
            else
            {
                writer.WriteStartArray(propertyName);
            }

            foreach (JToken item in array)
            {
                switch (item.Type)
                {
                    case JTokenType.Integer:
                        WriteNumberValue(writer, item);
                        break;
                    case JTokenType.Float:
                        writer.WriteNumberValue(item.ToObject<float>());
                        break;
                    case JTokenType.Boolean:
                        writer.WriteBooleanValue(item.ToObject<bool>());
                        break;
                    case JTokenType.String:
                        writer.WriteStringValue(item.ToString());
                        break;
                    case JTokenType.Date:
                        writer.WriteStringValue(item.ToObject<DateTime>().ToString("yyyy-MM-dd HH:mm:ss"));
                        break;
                    case JTokenType.Null:
                        writer.WriteNullValue();
                        break;
                    case JTokenType.Object:
                        new JObjectConverter().Write(writer, item.ToObject<JObject>(), options);
                        break;
                    case JTokenType.Array:
                        Write(writer, item.ToObject<JArray>(), options);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            writer.WriteEndArray();
        }

        private static void WriteNumberValue(Utf8JsonWriter writer, JToken item)
        {
            if (int.TryParse(item.ToString(), out var result))
            {
                writer.WriteNumberValue(result);
            }
            else
            {
                writer.WriteNumberValue(item.ToObject<long>());
            }
        }
    }
}