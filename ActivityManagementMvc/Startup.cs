using ActivityManagement.DataLayer.Context;
using ActivityManagement.IocConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.EntityFrameworkCore;

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
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddDbContext<ActivityManagementContext>(options=>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddCustomIdentityServices();
            services.AddCustomServices();
            services.ConfigureWritable<SiteSettings>(Configuration.GetSection("SiteSettings"));
            services.AddMvc();
            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/UserManager/AccessDenied";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseCustomIdentityServices();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
