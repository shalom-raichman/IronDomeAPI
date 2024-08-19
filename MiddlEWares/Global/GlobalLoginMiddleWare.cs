namespace IronDomeAPI.MiddleWares.Global
{
    public class GlobalLoginMiddleWare
    {
        private readonly RequestDelegate _next;

        public GlobalLoginMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            Console.WriteLine($"Got request to server: {request.Method} {request.Path}\n" + 
                $"From IP: {request.HttpContext.Connection.RemoteIpAddress}");

            await this._next(context);
        }
    }
}
