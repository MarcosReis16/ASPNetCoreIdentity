using AspNetCoreIdentity.Config;
using AspNetCoreIdentity.Extensions;
using AspNetCoreIdentity.Modulos.Identity.Data;
using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();


            if (hostingEnvironment.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        

        public void ConfigureServices(IServiceCollection services)
        {

            //Estão vindo da pasta Config
            services.AddIdentityConfig(Configuration);
            services.AddAuthorizationConfig();
            services.ResolveDependencies();
            services.AddKissLoggerConfig();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AuditoriaFilter));
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Tratamento de erro
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }
            
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            new KissLoggerConfiguration().RegisterKissLoggerListeners(Configuration, app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default",template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
