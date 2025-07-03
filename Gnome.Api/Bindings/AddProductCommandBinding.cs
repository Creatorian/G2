using Gnome.Application.G2.Query.AddProduct;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class AddProductCommandBinding : ILinqModelBinderConfiguration<AddProductCommand>
    {
        public void Configure(ModelBinderBuilder<AddProductCommand> builder)
        {
            builder.ForMember(x => x.Name)
                .FromForm()
                .HasParameterName("name");

            builder.ForMember(x => x.Slug)
                .FromForm()
                .HasParameterName("slug");

            builder.ForMember(x => x.Description)
                .FromForm()
                .HasParameterName("description");

            builder.ForMember(x => x.CategoryIds)
                .FromForm()
                .HasParameterName("category-ids");
        }
    }
} 