using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.ListCategories;
using Gnome.Application.Shared;
using Gnome.Domain.Common;
using Gnome.Domain.Responses;

namespace Gnome.Api.Bindings
{
    /// <summary>
    /// 
    /// </summary>
    public class ListCategoriesCommandBinding : PagedQueryBinding<ListCategoriesQueryCommand, SortedPagedList<CategoryListResponse>>, ILinqModelBinderConfiguration<ListCategoriesQueryCommand>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// 
        public override void Configure(ModelBinderBuilder<ListCategoriesQueryCommand> builder)
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
