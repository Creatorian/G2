using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.AddProduct;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class AddProductCommandBinding : BindingBase<AddProductCommand, int>, ILinqModelBinderConfiguration<AddProductCommand>
    {
        public override void Configure(ModelBinderBuilder<AddProductCommand> builder)
        {
            base.Configure(builder);

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