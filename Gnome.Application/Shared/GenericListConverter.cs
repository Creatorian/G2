using System.Collections.Generic;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Gnome.Application.Shared
{
    public class GenericListConverter : JsonConverter<object>
    {
        //
        // Summary:
        //     Check if type is generic list
        //
        // Parameters:
        //   type:
        public override bool CanConvert(Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() == typeof(List<>);
            }

            return false;
        }

        //
        // Summary:
        //     Reads generic list value
        //
        // Parameters:
        //   reader:
        //
        //   typeToConvert:
        //
        //   options:
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Type elementType = GetElementType(typeToConvert);
            return ReadArray(ref reader, typeToConvert, elementType, options);
        }

        //
        // Summary:
        //     Writes generic list value
        //
        // Parameters:
        //   writer:
        //
        //   value:
        //
        //   options:
        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (object item in (value as IEnumerable).Cast<object>().ToList())
            {
                JsonSerializer.Serialize(writer, item, item.GetType(), options);
            }

            writer.WriteEndArray();
        }

        private IList ReadArray(ref Utf8JsonReader reader, Type targetType, Type elementType, JsonSerializerOptions serializer)
        {
            IList list = CreateCompatibleList(targetType, elementType);
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                list.Add(JsonSerializer.Deserialize(ref reader, elementType, serializer));
            }

            if (!targetType.IsArray)
            {
                return list;
            }

            Array array = Array.CreateInstance(elementType, list.Count);
            list.CopyTo(array, 0);
            return array;
        }

        private static Type GetElementType(Type arrayOrGenericContainer)
        {
            if (arrayOrGenericContainer.IsArray)
            {
                return arrayOrGenericContainer.GetElementType();
            }

            return GetGenericTypeArguments(arrayOrGenericContainer).FirstOrDefault();
        }

        private static IEnumerable<Type> GetGenericTypeArguments(Type type)
        {
            return type.GenericTypeArguments;
        }

        private static IList CreateCompatibleList(Type targetContainerType, Type elementType)
        {
            TypeInfo typeInfo = ToTypeInfo(targetContainerType);
            if (typeInfo.IsArray || typeInfo.IsAbstract)
            {
                return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            }

            return (IList)Activator.CreateInstance(targetContainerType);
        }

        internal static TypeInfo ToTypeInfo(Type type)
        {
            return type?.GetTypeInfo();
        }
    }
}