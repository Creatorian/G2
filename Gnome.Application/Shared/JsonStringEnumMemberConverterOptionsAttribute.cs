using System.Text.Json;
using System;

namespace Gnome.Application.Shared
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class JsonStringEnumMemberConverterOptionsAttribute : Attribute
    {
        //
        // Summary:
        //
        public JsonStringEnumMemberConverterOptions Options { get; }

        //
        // Summary:
        //
        // Parameters:
        //   namingPolicyType:
        //     Optional type of a System.Text.Json.JsonNamingPolicy to use for writing enum
        //     values. Type must expose a public parameterless constructor.
        //
        //   allowIntegerValues:
        //     True to allow undefined enum values. When true, if an enum value isn't defined
        //     it will output as a number rather than a string.
        //
        //   deserializationFailureFallbackValue:
        //     Optional default value to use when a json string does not match anything defined
        //     on the target enum. If not specified a System.Text.Json.JsonException is thrown
        //     for all failures.
        public JsonStringEnumMemberConverterOptionsAttribute(Type namingPolicyType = null, bool allowIntegerValues = true, object deserializationFailureFallbackValue = null)
        {
            Options = new JsonStringEnumMemberConverterOptions
            {
                AllowIntegerValues = allowIntegerValues,
                DeserializationFailureFallbackValue = deserializationFailureFallbackValue
            };
            if (namingPolicyType != null)
            {
                if (!typeof(JsonNamingPolicy).IsAssignableFrom(namingPolicyType))
                {
                    throw new InvalidOperationException($"Supplied namingPolicyType {namingPolicyType} does not derive from JsonNamingPolicy.");
                }

                if (namingPolicyType.GetConstructor(Type.EmptyTypes) == null)
                {
                    throw new InvalidOperationException($"Supplied namingPolicyType {namingPolicyType} does not expose a public parameterless constructor.");
                }

                Options.NamingPolicy = (JsonNamingPolicy)Activator.CreateInstance(namingPolicyType);
            }
        }
    }
}