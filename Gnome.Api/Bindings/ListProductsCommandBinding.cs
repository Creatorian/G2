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

            builder.ForMember(x => x.Description)
                .HasParameterName("description")
                .FromQuery();

            builder.ForMember(x => x.ShortDescription)
                .HasParameterName("short-description")
                .FromQuery();

            builder.ForMember(x => x.NumberOfPlayers)
                .HasParameterName("number-of-players")
                .FromQuery();

            builder.ForMember(x => x.PlayingTime)
                .HasParameterName("playing-time")
                .FromQuery();

            builder.ForMember(x => x.CommunityAge)
                .HasParameterName("community-age")
                .FromQuery();

            builder.ForMember(x => x.Complexity)
                .HasParameterName("complexity")
                .FromQuery();

            // Numeric range filters
            builder.ForMember(x => x.MinRating)
                .HasParameterName("min-rating")
                .FromQuery();

            builder.ForMember(x => x.MaxRating)
                .HasParameterName("max-rating")
                .FromQuery();

            builder.ForMember(x => x.MinPrice)
                .HasParameterName("min-price")
                .FromQuery();

            builder.ForMember(x => x.MaxPrice)
                .HasParameterName("max-price")
                .FromQuery();

            builder.ForMember(x => x.MinStock)
                .HasParameterName("min-stock")
                .FromQuery();

            builder.ForMember(x => x.MaxStock)
                .HasParameterName("max-stock")
                .FromQuery();

            // Array filters
            builder.ForMember(x => x.Awards)
                .HasParameterName("awards")
                .FromQuery();

            builder.ForMember(x => x.CategoryIds)
                .HasParameterName("category-ids")
                .FromQuery();

            builder.ForMember(x => x.CategoryNames)
                .HasParameterName("category-names")
                .FromQuery();

            // Boolean filters
            builder.ForMember(x => x.HasImages)
                .HasParameterName("has-images")
                .FromQuery();

            builder.ForMember(x => x.InStockOnly)
                .HasParameterName("in-stock-only")
                .FromQuery();
        }
    }
}
