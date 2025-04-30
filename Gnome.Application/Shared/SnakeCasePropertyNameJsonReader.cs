using Newtonsoft.Json;
using System.IO;

namespace Gnome.Application.Shared
{
    public class SnakeCasePropertyNameJsonReader : JsonTextReader
    {
        public override object Value
        {
            get
            {
                if (TokenType == JsonToken.PropertyName)
                {
                    return ((string)base.Value).ToSnakeCase();
                }
                return base.Value;
            }
        }

        public SnakeCasePropertyNameJsonReader(TextReader textReader) : base(textReader) { }
    }
}