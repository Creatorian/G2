using Gnome.Application.G2.Query.AddVariant;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class AddVariantCommandBinding : ILinqModelBinderConfiguration<AddVariantCommand>
    {
        public void Configure(ModelBinderBuilder<AddVariantCommand> builder)
        {
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