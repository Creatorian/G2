using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Gnome.Application.Shared
{
    public class ValidationError : Problem
    {
        public string Tag { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }

        public ValidationError() { }

        public ValidationError(string tag, ValidationErrorCode error, string message)
        {
            string name = Enum.GetName(typeof(ValidationErrorCode), error);
            EnumMemberAttribute enumMemberAttribute = ((EnumMemberAttribute[])typeof(ValidationErrorCode).GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), inherit: true)).Single();
            Tag = tag;
            Error = enumMemberAttribute.Value;
            Message = message;
        }
    }
}