using System;
using API.Base.Emailing;
using API.Base.Files;
using API.Base.Logging;
using API.Base.Web.Base;
using API.Base.Web.Base.ApiBuilder;
using API.Base.Web.Base.Database;
using API.Base.Web.Common;
using API.Base.Web.RazorGenerator;
using API.StartApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.StartApp
{
    public class Startup
    {
        private ApiBuilder _apiBuilder;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _apiBuilder = new ApiBuilder(typeof(Startup).Assembly, configuration)
                .AddSpecifications<AuthApiSpecifications>()
                .AddSpecifications<LoggingApiSpecifications>()
                .AddSpecifications<FileApiSpecifications>()
                .AddSpecifications<EmailingApiSpecifications>()
                .AddSpecifications<WebCommonApiSpecifications>()
                .AddSpecifications<RazorGeneratorApiSpecifications>()
                .AddSpecifications<StarterApiSpecifications>()
//                .AddSpecifications<UniHackApiSpecifications>()
                .UseMySql<ApplicationDbContext>()
                .UseSwagger(new SwaggerSpecs
                {
                    Name = "startapp",
                    Title = "StartApp API",
                    Version = "v1",
//                    IndexStreamAction = () =>
//                        typeof(Startup).Assembly.GetManifestResourceStream(
//                            "API.StartApp.wwwroot.swagger_files.index.html"),
                });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<BaseDbContext, ApplicationDbContext>();

            _apiBuilder.BuildServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IDataSeeder dbSeeder, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/api/admin/Home/Error");
//                 The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();


            _apiBuilder.BuildApp(app, env, applicationLifetime, dbSeeder, serviceProvider);
        }
    }
}