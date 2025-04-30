namespace Gnome.Application.Shared
{
    public interface ILinqModelBinderConfiguration<T>
    {
        void Configure(ModelBinderBuilder<T> builder);
    }
}