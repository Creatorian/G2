using Gnome.Application.Shared;

namespace Gnome.Api.Bindings.Common
{
    public abstract class BindingBase<T, TOut> : ILinqModelBinderConfiguration<T> where T : CommandOrQueryBase<TOut>
    {
        public virtual void Configure(ModelBinderBuilder<T> builder)
        {
            builder.ForMember(x => x.ContentType)
                .FromHeader()
                .HasParameterName("Content-Type");

            builder.ForMember(x => x.Date)
                .FromHeader()
                .HasParameterName("Date");
        }
    }
}
