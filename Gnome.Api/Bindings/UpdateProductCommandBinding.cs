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

            builder.ForMember(x => x.ShortDescription)
                .FromForm()
                .HasParameterName("short-description");

            builder.ForMember(x => x.NumberOfPlayers)
                .FromForm()
                .HasParameterName("number-of-players");


            builder.ForMember(x => x.PlayingTime)
                .FromForm()
                .HasParameterName("playing-time");


            builder.ForMember(x => x.CommunityAge)
                .FromForm()
                .HasParameterName("community-age");


            builder.ForMember(x => x.Complexity)
                .FromForm()
                .HasParameterName("complexity");


            builder.ForMember(x => x.Rating)
                .FromForm()
                .HasParameterName("rating");

            builder.ForMember(x => x.Price)
                .FromForm()
                .HasParameterName("price");

            builder.ForMember(x => x.Stock)
                .FromForm()
                .HasParameterName("stock");

            builder.ForMember(x => x.AwardsString)
                .FromForm()
                .HasParameterName("awards");

            builder.ForMember(x => x.CategoryIds)
                .FromForm()
                .HasParameterName("category-ids");

            builder.ForMember(x => x.Images)
                .FromFormFile()
                .HasParameterName("images");
        }
    }
} 