using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Gnome.Application.Shared
{
    public class FormatterUtilities
    {
        public static JsonSerializerOptions GetSerializerOptions(IHttpContextAccessor _contextAccessor)
        {
            return GetSerializerOptions(_contextAccessor?.HttpContext);
        }

        public static JsonSerializerOptions GetSerializerOptions(HttpContext? _context)
        {
            if (_context == null)
            {
                return SystemTextSerializationOptions.KebabCaseSerializerOptions;
            }

            string text = _context.Request.Query[SerializationConstants.CASING_QUERY_PARAM].ToString();
            if (string.IsNullOrEmpty(text))
            {
                return SystemTextSerializationOptions.KebabCaseSerializerOptions;
            }

            return text switch
            {
                "camel" => SystemTextSerializationOptions.CamelCaseSerializerOptions,
                "kebab" => SystemTextSerializationOptions.KebabCaseSerializerOptions,
                "snake" => SystemTextSerializationOptions.SnakeCaseSerializerOptions,
                "pascal" => SystemTextSerializationOptions.PascalCaseSerializerOptions,
                _ => SystemTextSerializationOptions.KebabCaseSerializerOptions,
            };
        }
    }
}