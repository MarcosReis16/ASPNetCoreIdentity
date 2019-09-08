using AspNetCoreIdentity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies (this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();
            services.AddScoped<AuditoriaFilter>();

            return services;
        }
    }
}
