using System.Text.Json;
using System;

namespace Gnome.Application.Shared
{
    public class JsonStringEnumMemberConverterOptions
    {
        private object _DeserializationFailureFallbackValue;

        //
        // Summary:
        //     Gets or sets the optional System.Text.Json.JsonNamingPolicy for writing enum
        //     values.
        public JsonNamingPolicy NamingPolicy { get; set; }

        //
        // Summary:
        //     Gets or sets a value indicating whether integer values are allowed for reading
        //     enum values. Default value: true.
        public bool AllowIntegerValues { get; set; } = true;


        //
        // Summary:
        //     Gets or sets the optional default value to use when a json string does not match
        //     anything defined on the target enum. If not specified a System.Text.Json.JsonException
        //     is thrown for all failures.
        public object DeserializationFailureFallbackValue
        {
            get
            {
                return _DeserializationFailureFallbackValue;
            }
            set
            {
                _DeserializationFailureFallbackValue = value;
                ConvertedDeserializationFailureFallbackValue = ConvertEnumValueToUInt64(value);
            }
        }

        internal ulong? ConvertedDeserializationFailureFallbackValue { get; private set; }

        //
        // Summary:
        //     Initializes a new instance of the Asseco.Utilities.Serialization.Converters.JsonStringEnumMemberConverterOptions
        //     class.
        //
        // Parameters:
        //   namingPolicy:
        //     Optional naming policy for writing enum values.
        //
        //   allowIntegerValues:
        //     True to allow undefined enum values. When true, if an enum value isn't defined
        //     it will output as a number rather than a string.
        //
        //   deserializationFailureFallbackValue:
        //     Optional default value to use when a json string does not match anything defined
        //     on the target enum. If not specified a System.Text.Json.JsonException is thrown
        //     for all failures.
        public JsonStringEnumMemberConverterOptions(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true, object deserializationFailureFallbackValue = null)
        {
            NamingPolicy = namingPolicy;
            AllowIntegerValues = allowIntegerValues;
            DeserializationFailureFallbackValue = deserializationFailureFallbackValue;
        }

        private static ulong? ConvertEnumValueToUInt64(object deserializationFailureFallbackValue)
        {
            if (deserializationFailureFallbackValue == null)
            {
                return null;
            }

            ulong? num5 = ((deserializationFailureFallbackValue is int num) ? new ulong?((ulong)num) : ((deserializationFailureFallbackValue is long value) ? new ulong?((ulong)value) : ((deserializationFailureFallbackValue is byte b) ? new ulong?(b) : ((deserializationFailureFallbackValue is short num2) ? new ulong?((ulong)num2) : ((deserializationFailureFallbackValue is uint num3) ? new ulong?(num3) : ((deserializationFailureFallbackValue is ulong value2) ? new ulong?(value2) : ((deserializationFailureFallbackValue is sbyte b2) ? new ulong?((ulong)b2) : ((!(deserializationFailureFallbackValue is ushort num4)) ? null : new ulong?(num4)))))))));
            num5 = num5;
            ulong value3;
            if (!num5.HasValue)
            {
                if (!(deserializationFailureFallbackValue is Enum @enum))
                {
                    throw new InvalidOperationException("Supplied deserializationFailureFallbackValue parameter is not an enum value.");
                }

                value3 = JsonStringEnumMemberConverter.GetEnumValue(Type.GetTypeCode(@enum.GetType()), deserializationFailureFallbackValue);
            }
            else
            {
                value3 = num5.GetValueOrDefault();
            }

            return value3;
        }
    }
}