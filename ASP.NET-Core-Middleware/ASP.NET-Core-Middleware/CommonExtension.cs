using ASP.NET_Core_Middleware.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ASP.NET_Core_Middleware
{
    public static class CommonExtension
    {
        public static IServiceCollection AddApiKeyAuth(this IServiceCollection services, IConfiguration config)
        {
            var aa = config.Get<AuthOptions>();
            return services.Configure<AuthOptions>(config.GetSection("Auth"));
        }

        public static IApplicationBuilder UseApiKeyAuth(this IApplicationBuilder app)
        {
            return app.UseWhen(x => x.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase),
                builder => {
                    builder.UseMiddleware<AuthMiddleware>();
                });
        }
    }
}
