using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Gnome.Application.Shared
{
    public class PascalCasePropertyNameJsonReader : JsonTextReader
    {
        public override object Value
        {
            get
            {
                if (TokenType == JsonToken.PropertyName)
                {
                    return ((string)base.Value).ToPascalCase();
                }
                return base.Value;
            }
        }

        public PascalCasePropertyNameJsonReader(TextReader textReader) : base(textReader) { }
    }
}