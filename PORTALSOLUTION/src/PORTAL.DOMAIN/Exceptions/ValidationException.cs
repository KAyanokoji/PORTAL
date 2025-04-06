namespace PORTAL.DOMAIN.Exceptions
{
    public abstract class ValidationException(
    string title,
    int statusCode,
    IDictionary<string, string[]> errors,
    string message = "One or more validation errors occurred") : ApplicationException(title, statusCode, message)
    {
        public IDictionary<string, string[]> Errors { get; } = errors;
    }
}
