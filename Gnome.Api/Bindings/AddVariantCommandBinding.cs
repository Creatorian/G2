using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.AddVariant;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class AddVariantCommandBinding : BindingBase<AddVariantCommand, int>, ILinqModelBinderConfiguration<AddVariantCommand>
    {
        public override void Configure(ModelBinderBuilder<AddVariantCommand> builder)
        {
            base.Configure(builder);

            builder.ForMember(x => x.Name)
                .FromForm()
                .HasParameterName("name");

            builder.ForMember(x => x.Slug)
                .FromForm()
                .HasParameterName("slug");

            builder.ForMember(x => x.Price)
                .FromForm()
                .HasParameterName("price");

            builder.ForMember(x => x.Stock)
                .FromForm()
                .HasParameterName("stock");

            builder.ForMember(x => x.IsPrimary)
                .FromForm()
                .HasParameterName("is-primary");

            builder.ForMember(x => x.ProductId)
                .FromForm()
                .HasParameterName("product-id");

            builder.ForMember(x => x.Image)
                .FromFormFile()
                .HasParameterName("image");
        }
    }
} 