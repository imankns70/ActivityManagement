﻿using System;
using System.IO;
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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace ActivityManagementApi
{

    public class Startup
    {
        
        public IConfiguration Configuration { get; }
        public IServiceProvider Services { get; }
        private readonly SiteSettings _siteSettings;
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
    

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
            services.AddCustomKendoUi();
            services.AddApiVersioning();
            services.AddSwagger();
            services.AddCustomAuthentication(_siteSettings);
            services.ConfigureWritable<SiteSettings>(Configuration.GetSection("SiteSettings"));
            services.AddAuthorization(options =>
               {
                   options.AddPolicy(ConstantPolicies.DynamicPermission, policy => policy.Requirements.Add(new DynamicPermissionRequirement()));
               });

            //services.AddAuthorization(opt =>
            //{
            ////example of a policy only for one role

            //    opt.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));


            ////example of a policy only for multiple role
            //    opt.AddPolicy("AccessBank", policy => policy.RequireRole("Admin","Bloger","Writer"));
            //});
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

            //appBuilder.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            appBuilder.UseCors(p => p.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            appBuilder.UseCustomIdentityServices();
            appBuilder.UseSwaggerAndUI();
            appBuilder.UseRouting();
            appBuilder.UseAuthorization();
            appBuilder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwroot")
            });
            appBuilder.UseEndpoints(end =>
            {
                end.MapControllers();

            });
        
        }
    }
}
