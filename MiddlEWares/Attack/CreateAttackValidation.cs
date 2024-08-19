using System.Text.Json;

namespace IronDomeAPI.MiddlEWares.Attack
{
    public class CreateAttackValidation
    {
        private readonly RequestDelegate _next;

        public CreateAttackValidation(RequestDelegate next)
        {
            _next = next;
        }

        public async Task invoke(HttpContext context)
        {
            var request = context.Request;
            string body = GetRequestBodyAsync(request.Body);
            if (!string.IsNullOrEmpty(body))
            {
                var document = JsonDocument.Parse(body);
                //if (!document.RootElement.TryGetGuid("origin")) 
                //{

                //}
            }

        }

        private string GetRequestBodyAsync(object body)
        {
            return "  ";
        }
    }
}
