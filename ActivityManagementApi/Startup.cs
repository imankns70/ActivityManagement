using System;
using ActivityManagement.DataLayer.Context;
using ActivityManagement.IocConfig;
using ActivityManagement.IocConfig.Api.Middlewares;
using ActivityManagement.IocConfig.Api.Swagger;
using ActivityManagement.Services.EfInterfaces.Business;
using ActivityManagement.Services.EfServices.Business;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityManagementApi
{

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IServiceProvider Services { get; }
        private readonly SiteSettings SiteSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SiteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SiteSettings>();
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddDbContext<ActivityManagementContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
 
            
            services.AddCustomServices();
            services.AddCustomIdentityServices();
            services.AddCustomServices();
            services.AddApiVersioning();
            services.AddSwagger();
            services.AddCustomAuthentication(SiteSettings);
            services.ConfigureWritable<SiteSettings>(Configuration.GetSection("SiteSettings"));
            services.AddAuthorization(options =>
               {
                   options.AddPolicy(ConstantPolicies.DynamicPermission, policy => policy.Requirements.Add(new DynamicPermissionRequirement()));
               });
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
        {
            //var cachePeriod = env.IsDevelopment() ? "600" : "605800";
            appBuilder.UseCustomExceptionHandler();

            //if (env.IsDevelopment())
            //    appBuilder.UseDeveloperExceptionPage();

            appBuilder.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            appBuilder.UseCustomIdentityServices();
            appBuilder.UseSwaggerAndUI();
            appBuilder.UseRouting();
            appBuilder.UseAuthorization();
            appBuilder.UseEndpoints(end =>
            {
                end.MapControllers();
              
            });
        }
    }
}
