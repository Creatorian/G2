using Gnome.Application.G2.Query.AddProductImage;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class AddImageCommandBinding : ILinqModelBinderConfiguration<AddProductImageCommand>
    {
        public void Configure(ModelBinderBuilder<AddProductImageCommand> builder)
        {
            builder.ForMember(x => x.ProductId)
                .FromQuery()
                .HasParameterName("productId");

            builder.ForMember(x => x.SetAsPrimary)
                .FromQuery()
                .HasParameterName("setAsPrimary")
                .HasDefaultValue(false);

            builder.ForMember(x => x.Images)
                .FromFormFile()
                .HasParameterName("images");
        }
    }
} 