namespace PORTAL.SHARED.Utils
{
    public class JsonResponse
    {
        public bool IsSuccess { get; set; }
        public object? ResponseData { get; set; }  // Changed `dynamic` to `object`
        public string Message { get; set; } = string.Empty; // Ensures non-null Message
        public int StatusCode { get; set; }

        public bool IsToken { get; set; } = false; // Auto-property
        public string Token { get; set; } = string.Empty; // Auto-property
        public string? RedirectLocation { get; set; } // Add this for redirection support

    }
}
