using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Gnome.Application.Shared
{
    public class CaseUtilities
    {
        public static JToken DeserializeWithKebabPropertyNames(string json)
        {
            using TextReader textReader = new StringReader(json);
            using JsonReader reader = new KebabCasePropertyNameJsonReader(textReader);
            return new JsonSerializer().Deserialize<JToken>(reader);
        }

        public static JToken DeserializeWithCasing(string json, string casing)
        {
            using TextReader textReader = new StringReader(json);
            JsonReader jsonReader = null;
            jsonReader = casing switch
            {
                "pascal" => new PascalCasePropertyNameJsonReader(textReader),
                "camel" => new CamelCasePropertyNameJsonReader(textReader),
                "snake" => new SnakeCasePropertyNameJsonReader(textReader),
                "kebab" => new KebabCasePropertyNameJsonReader(textReader),
                _ => new KebabCasePropertyNameJsonReader(textReader),
            };
            using (jsonReader)
            {
                return new JsonSerializer().Deserialize<JToken>(jsonReader);
            }
        }

        //
        // Summary:
        //     Converts from Kebab case Json to Object type T
        //
        // Parameters:
        //   json:
        //
        // Type parameters:
        //   T:
        public static T ConvertFromJsonToObject<T>(string json)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = KebabCasePropertyNameResolver.Instance
            };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            return JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
        }

        //
        // Summary:
        //     Converts from kebab case Json to object Type type.
        //
        // Parameters:
        //   json:
        //
        //   type:
        public static object ConvertFromJsonToObject(string json, Type type)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = KebabCasePropertyNameResolver.Instance
            };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            return JsonConvert.DeserializeObject(json, type, jsonSerializerSettings);
        }

        //
        // Summary:
        //     Convert object to Kebab case json
        //
        // Parameters:
        //   obj:
        public static string ConvertFromObjectToJson(object obj)
        {
            return ConvertFromObjectToJson(obj, ignoreNullValues: false, ignoreLoopReferences: false);
        }

        //
        // Summary:
        //     Convert object to Kebab case json
        //
        // Parameters:
        //   obj:
        //
        //   ignoreNullValues:
        //
        //   ignoreLoopReferences:
        public static string ConvertFromObjectToJson(object obj, bool ignoreNullValues, bool ignoreLoopReferences)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = KebabCasePropertyNameResolver.Instance
            };
            if (ignoreLoopReferences)
            {
                jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }

            if (ignoreNullValues)
            {
                jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }

            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }
    }
}