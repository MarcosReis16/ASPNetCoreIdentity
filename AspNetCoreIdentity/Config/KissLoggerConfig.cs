using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentity.Config
{
    public static class KissLoggerConfig
    {
        public static IServiceCollection AddKissLoggerConfig(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(context => Logger.Factory.Get());

            return services;
        }
        
    }
}
