using Newtonsoft.Json.Serialization;

namespace Gnome.Application.Shared
{
    public class KebabCasePropertyNameResolver : DefaultContractResolver
    {
        private static readonly KebabCasePropertyNameResolver instance;

        //
        // Summary:
        //     Singleton instance of KebabCasePropertyNameResolver
        public static KebabCasePropertyNameResolver Instance => instance;

        static KebabCasePropertyNameResolver()
        {
            instance = new KebabCasePropertyNameResolver();
        }

        //
        // Summary:
        //     KebabCasePropertyNameResolver with KebabCaseNamingStrategy
        public KebabCasePropertyNameResolver()
        {
            base.NamingStrategy = new KebabCaseNamingStrategy
            {
                ProcessDictionaryKeys = true,
                OverrideSpecifiedNames = true
            };
        }
    }
}