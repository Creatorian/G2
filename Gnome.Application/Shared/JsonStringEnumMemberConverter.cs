using System.Collections.Generic;
using System.Reflection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gnome.Application.Shared
{
    public class JsonStringEnumMemberConverter : JsonConverterFactory
    {
        private class EnumMemberConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
        {
            private readonly JsonStringEnumMemberConverterHelper<TEnum> _JsonStringEnumMemberConverterHelper;

            public EnumMemberConverter(JsonStringEnumMemberConverterOptions options)
            {
                _JsonStringEnumMemberConverterHelper = new JsonStringEnumMemberConverterHelper<TEnum>(options);
            }

            public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                try
                {
                    return _JsonStringEnumMemberConverterHelper.Read(ref reader);
                }
                catch
                {
                    return default(TEnum);
                }
            }

            public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
            {
                _JsonStringEnumMemberConverterHelper.Write(writer, value);
            }
        }

        private class NullableEnumMemberConverter<TEnum> : JsonConverter<TEnum?> where TEnum : struct, Enum
        {
            private readonly JsonStringEnumMemberConverterHelper<TEnum> _JsonStringEnumMemberConverterHelper;

            public NullableEnumMemberConverter(JsonStringEnumMemberConverterOptions options)
            {
                _JsonStringEnumMemberConverterHelper = new JsonStringEnumMemberConverterHelper<TEnum>(options);
            }

            public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                try
                {
                    return _JsonStringEnumMemberConverterHelper.Read(ref reader);
                }
                catch
                {
                    return null;
                }
            }

            public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
            {
                _JsonStringEnumMemberConverterHelper.Write(writer, value.Value);
            }
        }

        private readonly HashSet<Type> _EnumTypes;

        private readonly JsonStringEnumMemberConverterOptions _Options;

        //
        // Summary:
        //
        public JsonStringEnumMemberConverter()
        {
        }

        //
        // Summary:
        //
        // Parameters:
        //   namingPolicy:
        //     Optional naming policy for writing enum values.
        //
        //   allowIntegerValues:
        //     True to allow undefined enum values. When true, if an enum value isn't defined
        //     it will output as a number rather than a string.
        public JsonStringEnumMemberConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
            : this(new JsonStringEnumMemberConverterOptions
            {
                NamingPolicy = namingPolicy,
                AllowIntegerValues = allowIntegerValues
            })
        {
        }

        //
        // Summary:
        //
        // Parameters:
        //   targetEnumTypes:
        //     Optional list of supported enum types to be converted. Specify null or empty
        //     to convert all enums.
        public JsonStringEnumMemberConverter(JsonStringEnumMemberConverterOptions options, params Type[] targetEnumTypes)
        {
            _Options = options ?? throw new ArgumentNullException("options");
            if (targetEnumTypes == null || targetEnumTypes.Length == 0)
            {
                return;
            }

            _EnumTypes = new HashSet<Type>(targetEnumTypes.Length);
            foreach (Type type in targetEnumTypes)
            {
                if (type.IsEnum)
                {
                    _EnumTypes.Add(type);
                    _EnumTypes.Add(typeof(Nullable<>).MakeGenericType(type));
                    continue;
                }

                if (type.IsGenericType)
                {
                    var (flag, item) = TestNullableEnum(type);
                    if (flag)
                    {
                        _EnumTypes.Add(item);
                        _EnumTypes.Add(type);
                        continue;
                    }
                }

                throw new NotSupportedException($"Type {type} is not supported by JsonStringEnumMemberConverter. Only enum types can be converted.");
            }
        }

        public override bool CanConvert(Type typeToConvert)
        {
            if (_EnumTypes == null)
            {
                if (!typeToConvert.IsEnum)
                {
                    if (typeToConvert.IsGenericType)
                    {
                        return TestNullableEnum(typeToConvert).IsNullableEnum;
                    }

                    return false;
                }

                return true;
            }

            return _EnumTypes.Contains(typeToConvert);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var (flag, type) = TestNullableEnum(typeToConvert);
            try
            {
                return flag ? ((JsonConverter)Activator.CreateInstance(typeof(NullableEnumMemberConverter<>).MakeGenericType(type), BindingFlags.Instance | BindingFlags.Public, null, new object[1] { _Options }, null)) : ((JsonConverter)Activator.CreateInstance(typeof(EnumMemberConverter<>).MakeGenericType(typeToConvert), BindingFlags.Instance | BindingFlags.Public, null, new object[1] { _Options }, null));
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }

                throw;
            }
        }

        private static (bool IsNullableEnum, Type UnderlyingType) TestNullableEnum(Type typeToConvert)
        {
            Type underlyingType = Nullable.GetUnderlyingType(typeToConvert);
            return (underlyingType?.IsEnum ?? false, underlyingType);
        }

        internal static ulong GetEnumValue(TypeCode enumTypeCode, object value)
        {
            return enumTypeCode switch
            {
                TypeCode.Int32 => (ulong)(int)value,
                TypeCode.Int64 => (ulong)(long)value,
                TypeCode.Int16 => (ulong)(short)value,
                TypeCode.Byte => (byte)value,
                TypeCode.UInt32 => (uint)value,
                TypeCode.UInt64 => (ulong)value,
                TypeCode.UInt16 => (ushort)value,
                TypeCode.SByte => (ulong)(sbyte)value,
                _ => throw new NotSupportedException($"Enum '{value}' of {enumTypeCode} type is not supported."),
            };
        }
    }
}