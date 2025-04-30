using System.Text.Json.Nodes;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Schema;

namespace Gnome.Application.Shared
{
    public class JSchemaConverter : JsonConverter<JSchema>
    {
        //
        // Summary:
        //     Compatiblity purposes Read method
        public override JSchema Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                return JSchema.Parse(JsonNode.Parse(ref reader).ToString());
            }
            catch (Exception)
            {
                throw new JsonException();
            }
        }

        //
        // Summary:
        //     Compatiblity purposes Write method
        public override void Write(Utf8JsonWriter writer, JSchema value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.ToString());
        }
    }
}