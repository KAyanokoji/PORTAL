using Microsoft.AspNetCore.Http;

namespace PORTAL.SHARED.Utils
{
    public static class ApiResponse
    {
        public static JsonResponse Success<T>(T? data, string message = "Success", int statusCode = 200)
        {
            return new JsonResponse 
            { 
                IsSuccess = true, 
                ResponseData = data, 
                Message = message, 
                StatusCode = statusCode 
            };
        }

        public static JsonResponse AuthenticationSuccess<T>(T token, string message = "Authentication successful")
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            return new JsonResponse 
            { 
                IsSuccess = true, 
                Token = token.ToString(), 
                Message = message, 
                StatusCode = 200, 
                IsToken = true 
            };
        }

        public static JsonResponse Created<T>(T? data, string message = "Resource created successfully")
        {
            return new JsonResponse 
            { 
                IsSuccess = true, 
                ResponseData = data, 
                Message = message, 
                StatusCode = StatusCodes.Status201Created 
            };
        }

        public static JsonResponse Updated<T>(T? data, string message = "Resource updated successfully")
        {
            return new JsonResponse 
            { 
                IsSuccess = true, 
                ResponseData = data, 
                Message = message, 
                StatusCode = StatusCodes.Status200OK 
            };
        }

        public static JsonResponse Error(string message, int statusCode = StatusCodes.Status400BadRequest)
        {
            return new JsonResponse 
            { 
                IsSuccess = false, 
                Message = message, 
                StatusCode = statusCode 
            };
        }

        public static JsonResponse NotFound(string message = "Requested resource not found")
        {
            return Error(message, StatusCodes.Status404NotFound);
        }

        public static JsonResponse Unauthorized(string message = "Unauthorized access")
        {
            return Error(message, StatusCodes.Status401Unauthorized);
        }

        public static JsonResponse Forbidden(string message = "Access to this resource is forbidden")
        {
            return Error(message, StatusCodes.Status403Forbidden);
        }

        public static JsonResponse ValidationError(IDictionary<string, string[]> errors, string message = "Validation errors occurred")
        {
            return new JsonResponse 
            { 
                IsSuccess = false, 
                ResponseData = errors, 
                Message = message, 
                StatusCode = StatusCodes.Status422UnprocessableEntity 
            };
        }

        public static JsonResponse InternalServerError(string message = "An unexpected error occurred")
        {
            return Error(message, StatusCodes.Status500InternalServerError);
        }

        public static JsonResponse Accepted(string message = "Request accepted for processing")
        {
            return new JsonResponse 
            { 
                IsSuccess = true, 
                Message = message, 
                StatusCode = StatusCodes.Status202Accepted 
            };
        }

        public static JsonResponse NoContent(string message = "No content available")
        {
            return new JsonResponse 
            { 
                IsSuccess = true, 
                Message = message, 
                StatusCode = StatusCodes.Status204NoContent 
            };
        }

        public static JsonResponse Redirect(string location, string message = "Redirecting to requested resource")
        {
            return new JsonResponse 
            { 
                IsSuccess = true, 
                Message = message, 
                StatusCode = StatusCodes.Status302Found, 
                RedirectLocation = location 
            };
        }
    }
}