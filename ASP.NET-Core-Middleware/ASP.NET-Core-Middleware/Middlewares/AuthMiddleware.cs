using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;

namespace ASP.NET_Core_Middleware.Middlewares
{
    public class AuthMiddleware
    {
        public const string ApiKeyHeader= "Mic-Api-Key";
        public const string ApiCey = "Abc";
        private readonly AuthOptions _authOptions;

        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next, IOptions<AuthOptions> apiAccesser)
        {
            _next = next;
            _authOptions = apiAccesser.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(!httpContext.Request.Headers.TryGetValue(ApiKeyHeader,out var apiKey))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsync("Mic-Api-Key header is missing");
                return;
            }
            if (ApiCey != apiKey)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("Invalid api key");
                return;
            }

            await _next(httpContext);
        }
    }
}
