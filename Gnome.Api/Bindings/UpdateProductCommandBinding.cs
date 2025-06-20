using Gnome.Application.G2.Query.UpdateProduct;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class UpdateProductCommandBinding : ILinqModelBinderConfiguration<UpdateProductCommand>
    {
        public void Configure(ModelBinderBuilder<UpdateProductCommand> builder)
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

            builder.ForMember(x => x.Description)
                .FromForm()
                .HasParameterName("description");

            builder.ForMember(x => x.CategoryId)
                .FromForm()
                .HasParameterName("category-id");
        }
    }
} 