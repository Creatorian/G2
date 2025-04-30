using System.Text.Json;

namespace Gnome.Application.Shared
{
    public class SystemTextSnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}