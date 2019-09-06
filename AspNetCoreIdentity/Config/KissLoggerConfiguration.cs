using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreIdentity.Config
{
    public class KissLoggerConfiguration
    {
        public void RegisterKissLoggerListeners(IConfiguration configuration, IApplicationBuilder app)
        {
            app.UseKissLogMiddleware(options =>
            {
                options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                    configuration["KissLog.OrganizationId"],
                    configuration["KissLog.ApplicationId"])
                ));
            });
        }
    }
}
