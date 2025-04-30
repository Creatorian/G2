using Gnome.Application.Shared;

namespace Gnome.Api.Bindings.Common
{
    public class PagedQueryBinding<T, TOut> : BindingBase<T, TOut>, ILinqModelBinderConfiguration<T>  where T : PagedQuery<TOut>
    {
        public override void Configure(ModelBinderBuilder<T> builder)
        {
            base.Configure(builder);

            builder.ForMember(x => x.Page)
                .FromQuery();

            builder.ForMember(x => x.PageSize)
                .FromQuery();

            builder.ForMember(x => x.SortBy)
                .FromQuery();

            builder.ForMember(x => x.SortOrder)
                .FromQuery();
        }
    }
}
