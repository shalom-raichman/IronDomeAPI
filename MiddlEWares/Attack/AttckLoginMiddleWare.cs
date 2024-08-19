namespace IronDomeAPI.MiddlEWares.Attack
{
    public class AttckLoginMiddleWare
    {
        private readonly RequestDelegate _next;
        public AttckLoginMiddleWare(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            Console.WriteLine($"inside Attack Login middleware");

            await this._next(context);
        }
        
        //public async Task InvokeAsync1(HttpContext context)
        //{
        //    var request = context.Request;
        //    if (request == null)
        //    {
        //        Console.WriteLine("object was null here");
        //        await this._next(context) ; 
        //    }
        //    else if (request.Method == "POST" && request.Body.Length > 10)
        //    {
        //        Console.WriteLine("i dont know anything");
        //        await this._next(context);
        //    }

        //    Console.WriteLine($"inside Attack Login middleware");

        //    await this._next(context);
        //}
    }
}
