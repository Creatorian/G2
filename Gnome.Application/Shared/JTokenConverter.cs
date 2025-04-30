using Newtonsoft.Json.Linq;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace Gnome.Application.Shared
{
    public class JTokenConverter : JsonConverter<JToken>
    {
        //
        // Summary:
        //     Compatiblity purposes Read method
        public override JToken Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                return new JObjectConverter().Read(ref reader, typeof(JObject), options);
            }

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                return new JArrayConverter().Read(ref reader, typeof(JArray), options);
            }

            throw new NotSupportedException();
        }

        //
        // Summary:
        //     Compatiblity purposes Write method
        public override void Write(Utf8JsonWriter writer, JToken value, JsonSerializerOptions options)
        {
            if (value.Type == JTokenType.Object)
            {
                new JObjectConverter().Write(writer, (JObject)value, options);
            }
            else if (value.Type == JTokenType.Array)
            {
                new JArrayConverter().Write(writer, (JArray)value, options);
            }
            else
            {
                new JValueConverter().Write(writer, value.Value<JValue>(), options);
            }
        }
    }
}