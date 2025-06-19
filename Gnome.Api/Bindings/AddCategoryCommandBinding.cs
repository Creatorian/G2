using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.AddCategory;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class AddCategoryCommandBinding : BindingBase<AddCategoryCommand, int>, ILinqModelBinderConfiguration<AddCategoryCommand>
    {
        public override void Configure(ModelBinderBuilder<AddCategoryCommand> builder)
        {
            base.Configure(builder);

            builder.ForMember(x => x.Name)
                .FromForm()
                .HasParameterName("name");

            builder.ForMember(x => x.Slug)
                .FromForm()
                .HasParameterName("slug");
        }
    }
} 