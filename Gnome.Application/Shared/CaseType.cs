using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public enum CaseType
    {
        //
        // Summary:
        //     Snake kase casing sample: configuration_entries
        SnakeCase,
        //
        // Summary:
        //     Kebab case casing sample: configuration-entries
        KebabCase,
        //
        // Summary:
        //     Pascal case casing sample: ConfigurationEntries
        PascalCase,
        //
        // Summary:
        //     Camel case casing sample: configurationEntries
        CamelCase,
        //
        // Summary:
        //     Default casing (Kebab-case)
        DefaultCase
    }
}
