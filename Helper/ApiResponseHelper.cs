using Microsoft.AspNetCore.Mvc;

namespace absensi_api.Helper
{
    public class ApiResponseHelper
    {
        public static ActionResult<T> Ok<T>(T data, string message = "Success")
        {
            if (data is not null)
            {
                return new ObjectResult(new
                {
                    StatusCode = 200,
                    Message = message,
                    Data = data
                });
            }
            else
            {
                return new ObjectResult(new
                {
                    StatusCode = 200,
                    Message = message,
                });
            }
        }
        public static ActionResult NoContent(string message = "No Content")
        {
            // No need to return 'Data' here as the response should not contain any content.
            return new ObjectResult(new
            {
                StatusCode = 204,
                Message = message
            });
        }

        public static ActionResult NotFound(string message = "Not Found")
        {
            return new ObjectResult(new
            {
                StatusCode = 404,
                Message = message
            });
        }
        public static ActionResult BadRequest(string message = "Bad Request")
        {
            return new ObjectResult(new
            {
                StatusCode = 400,
                Message = message
            });
        }
        public static ActionResult<T> Created<T>(T data, string message = "Created", int statusCode = 201)
        {
            return new ObjectResult(new
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            });
        }
        public static ActionResult InternalServerError(string message = "Internal Server Error")
        {
            return new ObjectResult(new
            {
                StatusCode = 500,
                Message = message,
            });
        }
        public static ActionResult Conflict(string message = "The request could not be completed due to a conflict with the current state of the resource.")
        {
            return new ObjectResult(new
            {
                StatusCode = 409,
                Message = message,
            });
        }
        public static ActionResult Unauthorized(string message = "The request could not be completed due to unauthorized access.")
        {
            return new ObjectResult(new
            {
                StatusCode = 401,
                Message = message,
            });
        }
        public static ActionResult Forbidden(string message = "The request could not be completed due to forbidden access.")
        {
            return new ObjectResult(new
            {
                StatusCode = 403,
                Message = message,
            });
        }

    }
}
