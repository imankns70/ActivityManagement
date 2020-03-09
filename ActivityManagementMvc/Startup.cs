using System;
using ActivityManagement.DataLayer.Context;
using ActivityManagement.IocConfig;
using ActivityManagement.IocConfig.Api.Middlewares;
using ActivityManagement.IocConfig.Api.Swagger;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityManagementMvc
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        private readonly SiteSettings SiteSettings;
        public IServiceProvider Services { get; }
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
            SiteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddDbContext<ActivityManagementContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddCustomIdentityServices();
            services.AddCustomServices();
            services.AddApiVersioning();
            services.AddCustomAuthentication(SiteSettings);
            services.AddSwagger();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstantPolicies.DynamicPermission, policy => policy.Requirements.Add(new DynamicPermissionRequirement()));
            });
            services.ConfigureWritable<SiteSettings>(Configuration.GetSection("SiteSettings"));
            services.AddMvc();

            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/UserManager/AccessDenied";
            });
            services.AddControllersWithViews().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                option.JsonSerializerOptions.PropertyNamingPolicy = null;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // once we use api and mvc with together
            //var cachePeriod = env.IsDevelopment() ? "600" : "605800";
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
            {
                appBuilder.UseCustomExceptionHandler();
            });

            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api"), appBuilder =>
            {
                if (env.IsDevelopment())
                    appBuilder.UseDeveloperExceptionPage();

                else
                    appBuilder.UseExceptionHandler("/Home/Error");
            });

            app.UseStaticFiles();
            app.UseCustomIdentityServices();
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Error404";
                    await next();
                }
            });
            app.UseSwaggerAndUI();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                    "areas",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}