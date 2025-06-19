using Gnome.Application.G2.Query.UpdateCategory;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class UpdateCategoryCommandBinding : ILinqModelBinderConfiguration<UpdateCategoryCommand>
    {
        public void Configure(ModelBinderBuilder<UpdateCategoryCommand> builder)
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
        }
    }
} 