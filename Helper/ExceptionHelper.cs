namespace absensi_api.Helper
{
    public static class ExceptionHelper
    {
        public static CustomHttpException CustomException(int statusCode, string message)
        {
            return new CustomHttpException(statusCode, message);
        }

        public class CustomHttpException : Exception
        {
            public int StatusCode { get; }

            public CustomHttpException(int statusCode, string message) : base(message)
            {
                StatusCode = statusCode;
            }
        }
    }
}
