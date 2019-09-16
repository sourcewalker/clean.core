using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.AutoMapper.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;
using System;
using System.Linq;

namespace Admin.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            // Automapper configuration
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new DomainMapperProfile());
                });
            services.AddSingleton(mappingConfig.CreateMapper());

            // Add runtime cache
            services.AddMemoryCache();

            // Add Hangfire services.
            services.AddHangfire(configuration =>
                configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(
                        Configuration.GetConnectionString("HangfireConnection"),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true
                        })
                    );

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // MVC Configuration
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddOpenApiDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHangfireDashboard();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var externalHostHeader = "X-External-Host";
            var externalPathHeader = "X-External-Path";
            app.UseOpenApi(config =>
            {
                config.PostProcess =
                    (document, request) =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "Admin Service REST Documentation";
                        document.Info.Description = "Admin Service API for Clean Core template";
                        document.Info.TermsOfService = "None";
                        document.Info.Contact = new NSwag.OpenApiContact
                        {
                            Name = "Irina Nalijaona",
                            Email = "nalijaona.andriamifidy@proximitybbdo.fr",
                            Url = "https://git.proximity.fr/nandriam"
                        };
                        document.Info.License = new NSwag.OpenApiLicense
                        {
                            Name = "Use under MIT License",
                            Url = "https://opensource.org/licenses/MIT"
                        };
                        if (request.Headers.ContainsKey(externalHostHeader))
                        {
                            document.Host = request.Headers[externalHostHeader].First();
                            document.BasePath = request.Headers[externalPathHeader].First();
                        }
                    };
            });
                

            app.UseSwaggerUi3(config =>
            {
                config.Path = "/swagger";
                config.TransformToExternalPath = (internalUiRoute, request) =>
                {
                    var externalPath = request.Headers.ContainsKey(externalPathHeader) ?
                        request.Headers[externalPathHeader].First() : "";
                    return $"{externalPath}{internalUiRoute}";
                };
            }
            );
            app.UseReDoc(options =>
            {
                options.Path = "/redoc";
            });
        }
    }
}
