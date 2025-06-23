namespace TechNest.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatusCode(statusCode);
        }

        private string GetMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Server Error",
                _ => "Unexpected Error",
            };
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
