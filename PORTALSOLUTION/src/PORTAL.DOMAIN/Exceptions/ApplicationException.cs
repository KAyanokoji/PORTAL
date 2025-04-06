
namespace PORTAL.DOMAIN.Exceptions
{
    public abstract class ApplicationException(string title, int statusCode, string message) : Exception(message)
    {
        public string Title { get; } = title;
        public int StatusCode { get; } = statusCode;
    }
}
