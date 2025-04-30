using Newtonsoft.Json;
using System.IO;

namespace Gnome.Application.Shared
{
    public class CamelCasePropertyNameJsonReader : JsonTextReader
    {
        public override object Value
        {
            get
            {
                if (TokenType == JsonToken.PropertyName)
                {
                    return ((string)base.Value).ToCamelCase();
                }
                return base.Value;
            }
        }

        public CamelCasePropertyNameJsonReader(TextReader textReader) : base(textReader) { }
    }
}