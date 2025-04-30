namespace Gnome.Application.Shared
{
    public interface IExceptionHttpResponse
    {
        int HttpStatusCode { get; }

        bool HasBody { get; }

        string GetHttpJsonResponse();
    }
}