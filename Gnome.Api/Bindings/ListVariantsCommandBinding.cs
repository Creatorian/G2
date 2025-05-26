using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.G2.Query.ListVariants;
using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Responses;

namespace Gnome.Api.Bindings
{
    public class ListVariantsCommandBinding : PagedQueryBinding<ListVariantsQueryCommand, SortedPagedList<VariantListResponse>>, ILinqModelBinderConfiguration<ListVariantsQueryCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// 
        public override void Configure(ModelBinderBuilder<ListVariantsQueryCommand> builder)
        {
            base.Configure(builder);

            builder.ForMember(x => x.DateFrom)
                .HasParameterName("date-from")
                .FromQuery();

            builder.ForMember(x => x.DateTo)
                .HasParameterName("date-to")
                .FromQuery();

            builder.ForMember(x => x.Name)
                .HasParameterName("name")
                .FromQuery();
        }
    }
}
