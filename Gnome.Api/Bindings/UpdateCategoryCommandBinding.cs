using Gnome.Api.Bindings.Common;
using Gnome.Application.G2.Query.UpdateCategory;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    public class UpdateCategoryCommandBinding : BindingBase<UpdateCategoryCommand, int>, ILinqModelBinderConfiguration<UpdateCategoryCommand>
    {
        public override void Configure(ModelBinderBuilder<UpdateCategoryCommand> builder)
        {
            base.Configure(builder);

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