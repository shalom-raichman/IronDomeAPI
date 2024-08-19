using Microsoft.AspNetCore.Mvc;

namespace IronDomeAPI.HttpUtils
{
    public class HttpUtils
    {
        public static object Respone(int status, object message)
        {
            bool success = status >= 200 && status < 300;
            return new
            {
                success = success,
                message = message
            };
        }
    }
}
