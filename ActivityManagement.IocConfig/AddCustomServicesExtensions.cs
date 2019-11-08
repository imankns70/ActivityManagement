using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.Services.EfServices.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace ActivityManagement.IocConfig
{
    public static class AddCustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {

            services.AddScoped<IEmailSender, EmailSender>();
            return services;
        }
    }
}