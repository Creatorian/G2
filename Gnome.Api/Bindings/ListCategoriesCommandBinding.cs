using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.ListCategories;
using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Responses;

namespace Gnome.Api.Bindings
{
    /// <summary>
    /// Binding configuration for ListCategoriesQueryCommand with comprehensive filtering support
    /// </summary>
    public class ListCategoriesCommandBinding : PagedQueryBinding<ListCategoriesQueryCommand, SortedPagedList<CategoryListResponse>>, ILinqModelBinderConfiguration<ListCategoriesQueryCommand>
    {

        /// <summary>
        /// Configure all filter properties for category listing
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(ModelBinderBuilder<ListCategoriesQueryCommand> builder)
        {
            base.Configure(builder);

            // Date range filters
            builder.ForMember(x => x.Filter.DateFrom)
                .HasParameterName("date-from")
                .FromQuery();

            builder.ForMember(x => x.Filter.DateTo)
                .HasParameterName("date-to")
                .FromQuery();

            // Text-based filters
            builder.ForMember(x => x.Filter.Name)
                .HasParameterName("name")
                .FromQuery();

            builder.ForMember(x => x.Filter.Slug)
                .HasParameterName("slug")
                .FromQuery();

            // Boolean filters
            builder.ForMember(x => x.Filter.HasProducts)
                .HasParameterName("has-products")
                .FromQuery();

            // Numeric range filters
            builder.ForMember(x => x.Filter.MinProductsCount)
                .HasParameterName("min-products-count")
                .FromQuery();

            builder.ForMember(x => x.Filter.MaxProductsCount)
                .HasParameterName("max-products-count")
                .FromQuery();
        }
    }
}
