using Gnome.Application.G2.Query.AddCategory;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class AddCategoryCommandBinding : ILinqModelBinderConfiguration<AddCategoryCommand>
    {
        public void Configure(ModelBinderBuilder<AddCategoryCommand> builder)
        {
            builder.ForMember(x => x.Name)
                .FromForm()
                .HasParameterName("name");

            builder.ForMember(x => x.Slug)
                .FromForm()
                .HasParameterName("slug");
        }
    }
} 