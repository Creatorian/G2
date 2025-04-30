using Newtonsoft.Json;
using System.IO;

namespace Gnome.Application.Shared
{
    public class KebabCasePropertyNameJsonReader : JsonTextReader
    {
        public override object Value
        {
            get
            {
                if (TokenType == JsonToken.PropertyName)
                {
                    return ((string)base.Value).ToKebabCase();
                }
                return base.Value;
            }
        }

        public KebabCasePropertyNameJsonReader(TextReader textReader) : base(textReader)  { }
    }
}