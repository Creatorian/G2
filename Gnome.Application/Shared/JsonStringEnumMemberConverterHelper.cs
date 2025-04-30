using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Gnome.Application.Shared
{
    internal class JsonStringEnumMemberConverterHelper<TEnum> where TEnum : struct, Enum
    {
        private class EnumInfo
        {
            public string Name;

            public TEnum EnumValue;

            public ulong RawValue;

            public EnumInfo(string name, TEnum enumValue, ulong rawValue)
            {
                Name = name;
                EnumValue = enumValue;
                RawValue = rawValue;
            }
        }

        private const BindingFlags EnumBindings = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        private const int MaximumAutoGrowthCacheSize = 64;

        private readonly bool _AllowIntegerValues;

        private readonly ulong? _DeserializationFailureFallbackValueRaw;

        private readonly TEnum? _DeserializationFailureFallbackValue;

        private readonly Type _EnumType;

        private readonly TypeCode _EnumTypeCode;

        private readonly bool _IsFlags;

        private readonly object _TransformedToRawCopyLockObject = new object();

        private readonly object _RawToTransformedCopyLockObject = new object();

        private Dictionary<TEnum, EnumInfo> _RawToTransformed;

        private Dictionary<string, EnumInfo> _TransformedToRaw;

        public JsonStringEnumMemberConverterHelper(JsonStringEnumMemberConverterOptions options)
        {
            _EnumType = typeof(TEnum);
            JsonStringEnumMemberConverterOptions jsonStringEnumMemberConverterOptions = _EnumType.GetCustomAttribute<JsonStringEnumMemberConverterOptionsAttribute>(inherit: false)?.Options ?? options;
            _AllowIntegerValues = jsonStringEnumMemberConverterOptions?.AllowIntegerValues ?? true;
            _EnumTypeCode = Type.GetTypeCode(_EnumType);
            _IsFlags = _EnumType.IsDefined(typeof(FlagsAttribute), inherit: true);
            ulong? num = jsonStringEnumMemberConverterOptions?.ConvertedDeserializationFailureFallbackValue;
            string[] enumNames = _EnumType.GetEnumNames();
            Array enumValues = _EnumType.GetEnumValues();
            int num2 = enumNames.Length;
            _RawToTransformed = new Dictionary<TEnum, EnumInfo>(num2);
            _TransformedToRaw = new Dictionary<string, EnumInfo>(num2);
            for (int i = 0; i < num2; i++)
            {
                Enum @enum = (Enum)enumValues.GetValue(i);
                if (@enum != null)
                {
                    ulong enumValue = JsonStringEnumMemberConverter.GetEnumValue(_EnumTypeCode, @enum);
                    string text = enumNames[i];
                    FieldInfo field = _EnumType.GetField(text, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    string text2 = field.GetCustomAttribute<EnumMemberAttribute>(inherit: true)?.Value ?? field.GetCustomAttribute<JsonPropertyNameAttribute>(inherit: true)?.Name ?? jsonStringEnumMemberConverterOptions?.NamingPolicy?.ConvertName(text) ?? text;
                    if (!(@enum is TEnum val))
                    {
                        throw new NotSupportedException();
                    }

                    if (num.HasValue && enumValue == num)
                    {
                        _DeserializationFailureFallbackValueRaw = num;
                        _DeserializationFailureFallbackValue = val;
                    }

                    _RawToTransformed[val] = new EnumInfo(text2, val, enumValue);
                    _TransformedToRaw[text2] = new EnumInfo(text, val, enumValue);
                }
            }

            if (num.HasValue && !_DeserializationFailureFallbackValue.HasValue)
            {
                throw new JsonException($"JsonStringEnumMemberConverter could not find a definition on Enum type {_EnumType} matching deserializationFailureFallbackValue '{num}'.");
            }
        }

        public TEnum Read(ref Utf8JsonReader reader)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    {
                        string @string = reader.GetString();
                        Dictionary<string, EnumInfo> transformedToRaw = _TransformedToRaw;
                        if (transformedToRaw.TryGetValue(@string, out var value9))
                        {
                            return value9.EnumValue;
                        }

                        if (_IsFlags)
                        {
                            ulong num = 0uL;
                            string[] array = @string.Split(", ");
                            foreach (string text in array)
                            {
                                if (transformedToRaw.TryGetValue(text, out value9))
                                {
                                    num |= value9.RawValue;
                                    continue;
                                }

                                bool flag = false;
                                foreach (KeyValuePair<string, EnumInfo> item in transformedToRaw)
                                {
                                    if (string.Equals(item.Key, text, StringComparison.OrdinalIgnoreCase))
                                    {
                                        num |= item.Value.RawValue;
                                        flag = true;
                                        break;
                                    }
                                }

                                if (!flag)
                                {
                                    if (!_DeserializationFailureFallbackValueRaw.HasValue)
                                    {
                                        throw new JsonException($"Unable to convert value '{text}' to enum type '{_EnumType}'.");
                                    }

                                    num |= _DeserializationFailureFallbackValueRaw.Value;
                                }
                            }

                            TEnum val = (TEnum)Enum.ToObject(_EnumType, num);
                            if (transformedToRaw.Count < 64)
                            {
                                lock (_TransformedToRawCopyLockObject)
                                {
                                    if (!_TransformedToRaw.ContainsKey(@string) && _TransformedToRaw.Count < 64)
                                    {
                                        Dictionary<string, EnumInfo> dictionary = new Dictionary<string, EnumInfo>(_TransformedToRaw);
                                        dictionary[@string] = new EnumInfo(@string, val, num);
                                        _TransformedToRaw = dictionary;
                                    }
                                }
                            }

                            return val;
                        }

                        foreach (KeyValuePair<string, EnumInfo> item2 in transformedToRaw)
                        {
                            if (string.Equals(item2.Key, @string, StringComparison.OrdinalIgnoreCase))
                            {
                                return item2.Value.EnumValue;
                            }
                        }

                        return _DeserializationFailureFallbackValue ?? throw new JsonException($"Unable to convert value '{@string}' to enum type '{_EnumType}'.");
                    }
                case JsonTokenType.Number:
                    if (_AllowIntegerValues)
                    {
                        switch (_EnumTypeCode)
                        {
                            case TypeCode.Int32:
                                {
                                    if (reader.TryGetInt32(out var value8))
                                    {
                                        return (TEnum)Enum.ToObject(_EnumType, value8);
                                    }

                                    break;
                                }
                            case TypeCode.Int64:
                                {
                                    if (reader.TryGetInt64(out var value4))
                                    {
                                        return (TEnum)Enum.ToObject(_EnumType, value4);
                                    }

                                    break;
                                }
                            case TypeCode.Int16:
                                {
                                    if (reader.TryGetInt16(out var value6))
                                    {
                                        return (TEnum)Enum.ToObject(_EnumType, value6);
                                    }

                                    break;
                                }
                            case TypeCode.Byte:
                                {
                                    if (reader.TryGetByte(out var value2))
                                    {
                                        return (TEnum)Enum.ToObject(_EnumType, value2);
                                    }

                                    break;
                                }
                            case TypeCode.UInt32:
                                {
                                    if (reader.TryGetUInt32(out var value7))
                                    {
                                        return (TEnum)Enum.ToObject(_EnumType, value7);
                                    }

                                    break;
                                }
                            case TypeCode.UInt64:
                                {
                                    if (reader.TryGetUInt64(out var value5))
                                    {
                                        return (TEnum)Enum.ToObject(_EnumType, value5);
                                    }

                                    break;
                                }
                            case TypeCode.UInt16:
                                {
                                    if (reader.TryGetUInt16(out var value3))
                                    {
                                        return (TEnum)Enum.ToObject(_EnumType, value3);
                                    }

                                    break;
                                }
                            case TypeCode.SByte:
                                {
                                    if (reader.TryGetSByte(out var value))
                                    {
                                        return (TEnum)Enum.ToObject(_EnumType, value);
                                    }

                                    break;
                                }
                        }

                        throw new JsonException($"Unable to convert value to enum type '{_EnumType}'.");
                    }

                    goto default;
                default:
                    if (_DeserializationFailureFallbackValue.HasValue)
                    {
                        reader.Skip();
                        return _DeserializationFailureFallbackValue.Value;
                    }

                    throw new JsonException($"Unable to convert value to enum type '{_EnumType}'.");
            }
        }

        public void Write(Utf8JsonWriter writer, TEnum value)
        {
            Dictionary<TEnum, EnumInfo> rawToTransformed = _RawToTransformed;
            if (rawToTransformed.TryGetValue(value, out var value2))
            {
                writer.WriteStringValue(value2.Name);
                return;
            }

            ulong enumValue = JsonStringEnumMemberConverter.GetEnumValue(_EnumTypeCode, value);
            if (_IsFlags)
            {
                ulong num = 0uL;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (KeyValuePair<TEnum, EnumInfo> item in rawToTransformed)
                {
                    value2 = item.Value;
                    if (value.HasFlag(value2.EnumValue) && value2.RawValue != 0L)
                    {
                        num |= value2.RawValue;
                        if (stringBuilder.Length > 0)
                        {
                            stringBuilder.Append(", ");
                        }

                        stringBuilder.Append(value2.Name);
                    }
                }

                if (num == enumValue)
                {
                    string text = stringBuilder.ToString();
                    if (rawToTransformed.Count < 64)
                    {
                        lock (_RawToTransformedCopyLockObject)
                        {
                            if (!_RawToTransformed.ContainsKey(value) && _RawToTransformed.Count < 64)
                            {
                                Dictionary<TEnum, EnumInfo> dictionary = new Dictionary<TEnum, EnumInfo>(_RawToTransformed);
                                dictionary[value] = new EnumInfo(text, value, enumValue);
                                _RawToTransformed = dictionary;
                            }
                        }
                    }

                    writer.WriteStringValue(text);
                    return;
                }
            }

            if (!_AllowIntegerValues)
            {
                throw new JsonException($"Enum type {_EnumType} does not have a mapping for integer value '{enumValue.ToString(CultureInfo.CurrentCulture)}'.");
            }

            switch (_EnumTypeCode)
            {
                case TypeCode.Int32:
                    writer.WriteNumberValue((int)enumValue);
                    break;
                case TypeCode.Int64:
                    writer.WriteNumberValue((long)enumValue);
                    break;
                case TypeCode.Int16:
                    writer.WriteNumberValue((short)enumValue);
                    break;
                case TypeCode.Byte:
                    writer.WriteNumberValue((byte)enumValue);
                    break;
                case TypeCode.UInt32:
                    writer.WriteNumberValue((uint)enumValue);
                    break;
                case TypeCode.UInt64:
                    writer.WriteNumberValue(enumValue);
                    break;
                case TypeCode.UInt16:
                    writer.WriteNumberValue((ushort)enumValue);
                    break;
                case TypeCode.SByte:
                    writer.WriteNumberValue((sbyte)enumValue);
                    break;
                default:
                    throw new JsonException();
            }
        }
    }
}