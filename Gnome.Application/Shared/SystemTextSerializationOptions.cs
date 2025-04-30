using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Gnome.Application.Shared
{
    public class SystemTextSerializationOptions
    {
        private static JsonNamingPolicy KebabCaseNamingPolicy = new SystemTextKebabCaseNamingPolicy();

        private static JsonNamingPolicy SnakeCaseNamingPolicy = new SystemTextSnakeCaseNamingPolicy();

        //
        // Summary:
        //     Kebab case serialization options
        public static JsonSerializerOptions KebabCaseSerializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
        {
            (JsonConverter)new JsonStringEnumMemberConverter(KebabCaseNamingPolicy),
            (JsonConverter)new BoolConverter(),
            (JsonConverter)new StringConverter(),
            (JsonConverter)new JObjectConverter(),
            (JsonConverter)new JTokenConverter(),
            (JsonConverter)new DateTimeConverter(),
            (JsonConverter)new GenericListConverter(),
            (JsonConverter)new JSchemaConverter()
        },
            PropertyNamingPolicy = KebabCaseNamingPolicy,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        //
        // Summary:
        //     Camel case serialization options
        public static JsonSerializerOptions CamelCaseSerializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
        {
            (JsonConverter)new JsonStringEnumMemberConverter(KebabCaseNamingPolicy),
            (JsonConverter)new BoolConverter(),
            (JsonConverter)new StringConverter(),
            (JsonConverter)new JTokenConverter(),
            (JsonConverter)new JObjectConverter(),
            (JsonConverter)new DateTimeConverter(),
            (JsonConverter)new GenericListConverter(),
            (JsonConverter)new JSchemaConverter()
        },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        //
        // Summary:
        //     Snake case serialization options
        public static JsonSerializerOptions SnakeCaseSerializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
        {
            (JsonConverter)new JsonStringEnumMemberConverter(KebabCaseNamingPolicy),
            (JsonConverter)new BoolConverter(),
            (JsonConverter)new StringConverter(),
            (JsonConverter)new JTokenConverter(),
            (JsonConverter)new JObjectConverter(),
            (JsonConverter)new DateTimeConverter(),
            (JsonConverter)new GenericListConverter(),
            (JsonConverter)new JSchemaConverter()
        },
            PropertyNamingPolicy = SnakeCaseNamingPolicy,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        //
        // Summary:
        //     Pascal case serialization options
        public static JsonSerializerOptions PascalCaseSerializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
        {
            (JsonConverter)new JsonStringEnumMemberConverter(KebabCaseNamingPolicy),
            (JsonConverter)new BoolConverter(),
            (JsonConverter)new StringConverter(),
            (JsonConverter)new DateTimeConverter(),
            (JsonConverter)new GenericListConverter()
        },
            PropertyNamingPolicy = null,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
    }
}