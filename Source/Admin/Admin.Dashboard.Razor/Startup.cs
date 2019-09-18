using Admin.Dashboard.Razor.Client;
using Admin.Dashboard.Razor.REST;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using GraphQL.Client;

namespace Admin.Dashboard.Razor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Uncomment to use Graph QL
            services.AddScoped(
                        x => new GraphQLClient(Configuration["ServiceUrl:AdminGraphQL"]));
            //services.AddScoped<IServiceClient, GraphQlClient>();

            services.AddHttpClient();
            services.AddHttpClient(
                        "RestClient",
                        client =>
                        {
                            client.BaseAddress = new Uri(Configuration["ServiceUrl:AdminREST"]);
                            client.DefaultRequestHeaders.Add("Accept", "application/json");
                        });
            services.AddScoped<IServiceClient, RESTClient>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
