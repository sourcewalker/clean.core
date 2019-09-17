using AutoMapper;
using Core.Infrastructure.Interfaces.Account;
using Core.Infrastructure.Interfaces.CRM;
using Core.Infrastructure.Interfaces.DAL;
using Core.Infrastructure.Interfaces.InstantWin;
using Core.Infrastructure.Interfaces.Logging;
using Core.Infrastructure.Interfaces.Mapping;
using Core.Infrastructure.Interfaces.Scheduler;
using Core.Infrastructure.Interfaces.Validator;
using Core.Service.Domain;
using Core.Service.Flow;
using Core.Service.Interfaces;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Infrastructure.AutoMapper.Profiles;
using Infrastructure.AutoMapper.Provider;
using Infrastructure.Captcha.Provider;
using Infrastructure.Community;
using Infrastructure.DAL.EF;
using Infrastructure.DAL.EF.Repository.Implementations;
using Infrastructure.Hangfire;
using Infrastructure.InstantWin.Provider;
using Infrastructure.NewRelic;
using Infrastructure.ProCampaign.Consumer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Web.Service.REST.Filters;

namespace Web.Service
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

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LocalDatabase")));

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

            //services.AddScoped<Core.Infrastructure.Interfaces.Configuration.IConfigurationProvider, ConfigurationProvider>();

            // Infrastructures
            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddScoped<IAccountProvider, KuhmunityProvider>();
            services.AddScoped<ILoggingProvider, LoggingProvider>();
            services.AddScoped<ISchedulerProvider, HangfireProvider>();
            services.AddScoped<ICrmConsumerProvider, ConsumerProvider>();
            services.AddScoped<IFormValidatorProvider, CaptchaProvider>();
            services.AddScoped<IInstantWinMomentProvider, InstantWinProvider>();

            // DAL
            services.AddScoped<IFailedTransactionRepository, FailedTransactionRepository>();
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IParticipationRepository, ParticipationRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();

            // Services
            services.AddScoped<IParticipationService, ParticipationService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IFailedTransactionService, FailedTransactionService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IJourneyService, JourneyService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<ILegalService, LegalService>();

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
                        document.Info.Title = "Web Service REST Documentation";
                        document.Info.Description = "Web Service API for Clean Core template";
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

            app.UseHangfireDashboard(
                pathMatch: "/hangfire"
                //options: new DashboardOptions()
                //{
                //    Authorization = new IDashboardAuthorizationFilter[] {
                //        new SchedulerAuthorizationFilter(Configuration)
                //    }
                //}
            );
        }
    }
}
