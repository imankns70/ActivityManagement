using ActivityManagement.Services.Api;
using ActivityManagement.Services.Api.Contract;
using ActivityManagement.Services.EfInterfaces.Business;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.Services.EfServices.Business;
using ActivityManagement.Services.EfServices.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace ActivityManagement.IocConfig
{
    public static class AddCustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddTransient<IjwtService, jwtService>();
            services.AddScoped<ITeamService, TeamService>();
            return services;
        }
    }
}