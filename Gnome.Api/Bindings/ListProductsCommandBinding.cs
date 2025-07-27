using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Responses;

namespace Gnome.Api.Bindings
{
    /// <summary>
    /// Binding configuration for ListProductsQueryCommand with comprehensive filtering support
    /// </summary>
    public class ListProductsCommandBinding : PagedQueryBinding<ListProductsQueryCommand, SortedPagedList<ProductListResponse>>, ILinqModelBinderConfiguration<ListProductsQueryCommand>
    {

        /// <summary>
        /// Configure all filter properties for product listing
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(ModelBinderBuilder<ListProductsQueryCommand> builder)
        {
            base.Configure(builder);

            // Date range filters
            builder.ForMember(x => x.DateFrom)
                .HasParameterName("date-from")
                .FromQuery();

            builder.ForMember(x => x.DateTo)
                .HasParameterName("date-to")
                .FromQuery();

            // Text-based filters
            builder.ForMember(x => x.Name)
                .HasParameterName("name")
                .FromQuery();

            builder.ForMember(x => x.Slug)
                .HasParameterName("slug")
                .FromQuery();

            builder.ForMember(x => x.MinPlayers)
                .HasParameterName("min-players")
                .FromQuery();

            builder.ForMember(x => x.MaxPlayers)
                .HasParameterName("max-players")
                .FromQuery();

            builder.ForMember(x => x.MinPlayingTime)
                .HasParameterName("min-playing-time")
                .FromQuery();

            builder.ForMember(x => x.MaxPlayingTime)
                .HasParameterName("max-playing-time")
                .FromQuery();

            builder.ForMember(x => x.Complexity)
                .HasParameterName("complexity")
                .FromQuery();

            builder.ForMember(x => x.Rating)
                .HasParameterName("rating")
                .FromQuery();

            builder.ForMember(x => x.MinPrice)
                .HasParameterName("min-price")
                .FromQuery();

            builder.ForMember(x => x.MaxPrice)
                .HasParameterName("max-price")
                .FromQuery();

            builder.ForMember(x => x.CategoryIds)
                .HasParameterName("category-ids")
                .FromQuery();

            builder.ForMember(x => x.CategorySlugs)
                .HasParameterName("category-slugs")
                .FromQuery();

            builder.ForMember(x => x.InStockOnly)
                .HasParameterName("in-stock-only")
                .FromQuery();
        }
    }
}
