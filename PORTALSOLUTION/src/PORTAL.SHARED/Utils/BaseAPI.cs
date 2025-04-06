namespace PORTAL.SHARED.Utils
{
    public static class BaseAPI
    {
        public static JsonResponse Go<T>(T? data, string message = "Success")
        {
            return new JsonResponse() { IsSuccess = true, ResponseData = data, Message = message, StatusCode = 200 };
        }

        public static JsonResponse Token<T>(string data, string message = "Login Successful")
        {
            return new JsonResponse() { IsSuccess = true, Token = data, Message = message, StatusCode = 200, IsToken=true };
        }

        public static JsonResponse Create<T>(T? data, string message = "Created successfully")
        {
            return new JsonResponse() { IsSuccess = true, ResponseData = data, Message = message, StatusCode = 201 };
        }

        public static JsonResponse Update<T>(T? data, string message = "Updated successfully")
        {
            return new JsonResponse() { IsSuccess = true, ResponseData = data, Message = message, StatusCode = 200 };
        }

        public static JsonResponse Error(string message, int statusCode = 400)
        {
            return new JsonResponse() { IsSuccess = false, ResponseData = null, Message = message, StatusCode = statusCode };
        }

        public static JsonResponse NotFound(string message = "Resource not found")
        {
            return new JsonResponse() { IsSuccess = false, ResponseData = null, Message = message, StatusCode = 404 };
        }

        public static JsonResponse Unauthorized(string message = "Unauthorized access")
        {
            return new JsonResponse() { IsSuccess = false, ResponseData = null, Message = message, StatusCode = 401 };
        }

        public static JsonResponse Forbidden(string message = "Forbidden access")
        {
            return new JsonResponse() { IsSuccess = false, ResponseData = null, Message = message, StatusCode = 403 };
        }

        public static JsonResponse ValidationError(IDictionary<string, string[]> errors)
        {
            return new JsonResponse() { IsSuccess = false, ResponseData = errors, Message = "One or more validation errors occurred.", StatusCode = 422 };
        }

        public static JsonResponse InternalServerError(string message = "An unexpected error occurred.")
        {
            return new JsonResponse() { IsSuccess = false, ResponseData = null, Message = message, StatusCode = 500 };
        }

        public static JsonResponse Accepted(string message = "Request is accepted and processing.")
        {
            return new JsonResponse() { IsSuccess = true, ResponseData = null, Message = message, StatusCode = 202 };
        }

        public static JsonResponse NoContent(string message = "No content")
        {
            return new JsonResponse() { IsSuccess = true, ResponseData = null, Message = message, StatusCode = 204 };
        }

        public static JsonResponse Redirect(string location)
        {
            return new JsonResponse() { IsSuccess = true, ResponseData = null, Message = "Redirecting to another location.", StatusCode = 302, RedirectLocation = location };
        }
    }
}
