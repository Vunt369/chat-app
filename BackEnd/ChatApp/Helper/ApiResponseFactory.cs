using ChatApp.Infrastructure.Response;

namespace ChatApp.Helper
{
    public static class ApiResponseFactory
    {
        public static ResponseResult<T> Success<T>(T data)
        {
            return new ResponseResult<T>(200, "Success", data);
        }
        public static ResponseResult<T> Fail<T>(string message, int statusCode = 400)
        {
            return new ResponseResult<T>(statusCode, message, default);
        }

    }
}
