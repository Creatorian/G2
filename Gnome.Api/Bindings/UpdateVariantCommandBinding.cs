using Gnome.Application.G2.Query.UpdateVariant;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class UpdateVariantCommandBinding : ILinqModelBinderConfiguration<UpdateVariantCommand>
    {
        public void Configure(ModelBinderBuilder<UpdateVariantCommand> builder)
        {
            builder.ForMember(x => x.Id)
                .FromForm()
                .HasParameterName("id");

            builder.ForMember(x => x.Name)
                .FromForm()
                .HasParameterName("name");

            builder.ForMember(x => x.Slug)
                .FromForm()
                .HasParameterName("slug");

            builder.ForMember(x => x.Image)
                .FromFormFile()
                .HasParameterName("image");

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
        }
    }
} 