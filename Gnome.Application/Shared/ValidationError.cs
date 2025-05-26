using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Gnome.Application.Shared
{
    public class ValidationError : Problem
    {
        public string Tag { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }

        private static readonly ConcurrentDictionary<ValidationErrorCode, string> _errorCodeCache = new();

        public ValidationError() { }

        public ValidationError(string tag, ValidationErrorCode error, string message)
        {
            Tag = tag;
            Error = GetEnumMemberValue(error);
            Message = message;
        }

        private static string GetEnumMemberValue(ValidationErrorCode error)
        {
            return _errorCodeCache.GetOrAdd(error, code =>
            {
                var type = typeof(ValidationErrorCode);
                var name = Enum.GetName(type, code);
                if (name == null)
                    throw new ArgumentException($"Invalid enum value: {code}", nameof(error));

                var field = type.GetField(name);
                var attr = field?.GetCustomAttribute<EnumMemberAttribute>(inherit: true);
                if (attr == null)
                    throw new InvalidOperationException($"Enum value '{name}' is missing EnumMemberAttribute.");

                return attr.Value;
            });
        }
    }
}