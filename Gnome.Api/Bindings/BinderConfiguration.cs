using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    /// <summary>
    /// 
    /// </summary>
    public class BinderConfiguration
    {
        public LinqModelBuilderConfiguration CreateConfiguration()
        {
            LinqModelBuilderConfiguration configuration = new();

            configuration.ApplyConfiguration(new ListProductsCommandBinding());

            return configuration;
        }
    }
}
