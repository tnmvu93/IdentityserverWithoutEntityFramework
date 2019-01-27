using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;
using IdentityServer4.Stores;
using Microsoft.ApplicationInsights.Extensibility;
using IdentityserverWithoutEntityFramework.Server.Persistence;

namespace IdentityserverWithoutEntityFramework.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(ConfigureIdentityServer.GetClients())
                .AddInMemoryIdentityResources(ConfigureIdentityServer.GetIdentityResources())
                .AddProfileService<UserProfileService>();

            services.AddSingleton<IUserStore, UserStore>();

            services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                    options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                try
                {
                    var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
                    configuration.DisableTelemetry = true;
                }
                catch { }
            }

            app.UseIdentityServer(); // includes a call to UseAuthentication

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
