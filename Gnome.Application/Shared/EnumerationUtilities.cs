using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System;
using System.Collections;

namespace Gnome.Application.Shared
{
    //
    // Summary:
    //     Utilities for working with Enumerations
    public class EnumerationUtilities
    {
        //
        // Summary:
        //     Gets Enum by EnumMember Attribute name
        //
        // Parameters:
        //   str:
        //
        // Type parameters:
        //   T:
        public static T? ToEnum<T>(string str) where T : struct
        {
            Type typeFromHandle = typeof(T);
            return (T?)ToEnum(str, typeFromHandle);
        }

        //
        // Summary:
        //     Gets Enum by EnumMember Attribute name
        //
        // Parameters:
        //   str:
        //
        //   enumType:
        public static object ToEnum(string str, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string text in names)
            {
                if (((EnumMemberAttribute[])enumType.GetField(text).GetCustomAttributes(typeof(EnumMemberAttribute), inherit: true)).Single().Value == str)
                {
                    return Enum.Parse(enumType, text);
                }
            }

            return null;
        }

        //
        // Summary:
        //     Converts Enum to String by getting EnumMember Attribute
        //
        // Parameters:
        //   type:
        //
        // Type parameters:
        //   T:
        public static string ToEnumString<T>(T type)
        {
            Type typeFromHandle = typeof(T);
            string name = Enum.GetName(typeFromHandle, type);
            return ((EnumMemberAttribute[])typeFromHandle.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), inherit: true)).Single().Value;
        }

        //
        // Summary:
        //     Gets List of Enum from Comma seperated string.
        //
        // Parameters:
        //   listString:
        //     Comma seperated list of strings
        //
        // Type parameters:
        //   T:
        //     Enumeration as Type
        public static List<T> GetEnumPropertiesForListString<T>(string listString) where T : struct
        {
            List<string> list = listString.Split(',').ToList();
            List<T> list2 = new List<T>();
            foreach (string item in list)
            {
                T? val = ToEnum<T>(item?.Trim());
                if (val.HasValue)
                {
                    list2.Add(val.Value);
                }
            }

            return list2;
        }

        //
        // Summary:
        //     Get list of Classification Schema from all enumrations registred in assembly
        //
        //
        // Parameters:
        //   serviceName:
        //     (Optional) Name of service for which enumerations are resolved for
        public static List<Classification> GetEnumerations(string serviceName = null)
        {
            List<Type> allEnums = GetAllEnums();
            List<Classification> list = new List<Classification>();
            foreach (Type item in allEnums)
            {
                Classification enumerationMetadataForType = GetEnumerationMetadataForType(item);
                if (enumerationMetadataForType.ServiceName == null || (serviceName != null && enumerationMetadataForType.ServiceName.Equals(serviceName)))
                {
                    list.Add(new Classification
                    {
                        Name = enumerationMetadataForType.Name,
                        SchemaId = enumerationMetadataForType.SchemaId,
                        Description = enumerationMetadataForType.Description
                    });
                }
            }

            return list;
        }

        //
        // Summary:
        //     Gets enumeration as Classification schema by ID of enum
        //
        // Parameters:
        //   enumId:
        //     ID of enumeration
        //
        //   serviceName:
        //     (Optional) Name of service for which enumeration is resolved for
        public static Classification GetEnumerationSchema(string enumId, string serviceName = null)
        {
            List<Type> allEnums = GetAllEnums();
            Type type = null;
            foreach (Type item in allEnums)
            {
                Classification enumerationMetadataForType = GetEnumerationMetadataForType(item);
                if (enumerationMetadataForType.SchemaId.Equals(enumId) && (enumerationMetadataForType.ServiceName == null || (serviceName != null && enumerationMetadataForType.ServiceName.Equals(serviceName))))
                {
                    type = item;
                    break;
                }
            }

            if (type != null)
            {
                List<ClassificationValue> enumerationValues = GetEnumerationValues(type);
                Classification enumerationMetadataForType2 = GetEnumerationMetadataForType(type);
                enumerationMetadataForType2.Values = enumerationValues;
                return enumerationMetadataForType2;
            }

            return null;
        }

        //
        // Summary:
        //     Joins the collection to comma separated string.
        //
        // Parameters:
        //   collection:
        //     The collection.
        public static string JoinCollection(IEnumerable collection)
        {
            if (collection == null)
            {
                return null;
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (object item in collection)
            {
                stringBuilder.Append(item.ToString()).Append(",");
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }

            return stringBuilder.ToString();
        }

        private static List<Type> GetAllEnums()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> list = new List<Type>();
            Assembly[] array = assemblies;
            foreach (Assembly assembly in array)
            {
                try
                {
                    list.AddRange((from t in assembly.GetTypes()
                                   where t.IsEnum && t.GetCustomAttribute<Enumeration>() != null
                                   select t).ToList());
                }
                catch
                {
                }
            }

            return list;
        }

        private static Classification GetEnumerationMetadataForType(Type t)
        {
            if (t == null)
            {
                return null;
            }

            Enumeration[] array = (Enumeration[])t.GetCustomAttributes(typeof(Enumeration), inherit: false);
            if (array.Length == 0)
            {
                return null;
            }

            return array[0].EnumerationMetaData;
        }

        private static List<ClassificationValue> GetEnumerationValues(Type type)
        {
            Array values = Enum.GetValues(type);
            string[] names = Enum.GetNames(type);
            if (values == null || names == null || values.Length != names.Length)
            {
                return null;
            }

            List<ClassificationValue> list = new List<ClassificationValue>();
            for (int i = 0; i < values.Length; i++)
            {
                Enum @enum = (Enum)Enum.Parse(type, values.GetValue(i).ToString(), ignoreCase: false);
                string text = (((EnumMemberAttribute[])type.GetField(names[i]).GetCustomAttributes(typeof(EnumMemberAttribute), inherit: true))?.FirstOrDefault())?.Value ?? @enum.ToString();
                list.Add(new ClassificationValue
                {
                    Literal = text,
                    Name = text,
                    Description = GetDescription(@enum)
                });
            }

            return list;
        }

        public static string GetDescription<T>(T e) where T : IConvertible
        {
            string result = null;
            if (e is Enum)
            {
                Type type = e.GetType();
                foreach (int value in Enum.GetValues(type))
                {
                    if (value == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        object[] customAttributes = type.GetMember(type.GetEnumName(value))[0].GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
                        if (customAttributes.Length != 0)
                        {
                            result = ((DescriptionAttribute)customAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            return result;
        }
    }
}