using ActivityManagement.DataLayer.Context;
using ActivityManagement.IocConfig;
using ActivityManagement.ViewModels.DynamicAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ActivityManagementMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            this.Configuration = configuration;
               
        }
                public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application,
        // visit https://go.microsoft.com/fwlink/?LinkID=398940


        public void ConfigureServices(IServiceCollection services)
        {


            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddDbContext<ActivityManagementContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddCustomIdentityServices();
            services.AddCustomServices();

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
            //services.AddControllersWithViews().AddJsonOptions(option =>
            //{
            //    option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            //    option.JsonSerializerOptions.PropertyNamingPolicy = null;

            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");



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
