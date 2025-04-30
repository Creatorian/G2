using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Responses;

namespace Gnome.Api.Bindings
{
    /// <summary>
    /// 
    /// </summary>
    public class ListProductsCommandBinding : PagedQueryBinding<ListProductsQueryCommand, SortedPagedList<ProductListResponse>>, ILinqModelBinderConfiguration<ListProductsQueryCommand>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// 
        public override void Configure(ModelBinderBuilder<ListProductsQueryCommand> builder)
        {
            base.Configure(builder);

            builder.ForMember(x => x.DateFrom)
                .HasParameterName("dateFrom")
                .FromQuery();

            builder.ForMember(x => x.DateTo)
                .HasParameterName("dateTo")
                .FromQuery();

            builder.ForMember(x => x.Name)
                .HasParameterName("name")
                .FromQuery();
        }
    }
}
