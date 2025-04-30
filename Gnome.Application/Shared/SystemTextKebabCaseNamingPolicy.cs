using System.Text.Json;

namespace Gnome.Application.Shared
{
    public class SystemTextKebabCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToKebabCase();
        }
    }
}