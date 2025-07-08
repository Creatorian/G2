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

            builder.ForMember(x => x.Filter.Description)
                .HasParameterName("description")
                .FromQuery();

            builder.ForMember(x => x.Filter.ShortDescription)
                .HasParameterName("short-description")
                .FromQuery();

            builder.ForMember(x => x.Filter.NumberOfPlayers)
                .HasParameterName("number-of-players")
                .FromQuery();

            builder.ForMember(x => x.Filter.PlayingTime)
                .HasParameterName("playing-time")
                .FromQuery();

            builder.ForMember(x => x.Filter.CommunityAge)
                .HasParameterName("community-age")
                .FromQuery();

            builder.ForMember(x => x.Filter.Complexity)
                .HasParameterName("complexity")
                .FromQuery();

            // Numeric range filters
            builder.ForMember(x => x.Filter.MinRating)
                .HasParameterName("min-rating")
                .FromQuery();

            builder.ForMember(x => x.Filter.MaxRating)
                .HasParameterName("max-rating")
                .FromQuery();

            builder.ForMember(x => x.Filter.MinPrice)
                .HasParameterName("min-price")
                .FromQuery();

            builder.ForMember(x => x.Filter.MaxPrice)
                .HasParameterName("max-price")
                .FromQuery();

            builder.ForMember(x => x.Filter.MinStock)
                .HasParameterName("min-stock")
                .FromQuery();

            builder.ForMember(x => x.Filter.MaxStock)
                .HasParameterName("max-stock")
                .FromQuery();

            // Array filters
            builder.ForMember(x => x.Filter.Awards)
                .HasParameterName("awards")
                .FromQuery();

            builder.ForMember(x => x.Filter.CategoryIds)
                .HasParameterName("category-ids")
                .FromQuery();

            builder.ForMember(x => x.Filter.CategoryNames)
                .HasParameterName("category-names")
                .FromQuery();

            // Boolean filters
            builder.ForMember(x => x.Filter.HasImages)
                .HasParameterName("has-images")
                .FromQuery();

            builder.ForMember(x => x.Filter.InStockOnly)
                .HasParameterName("in-stock-only")
                .FromQuery();
        }
    }
}
