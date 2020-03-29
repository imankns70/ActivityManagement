using Microsoft.Extensions.DependencyInjection;

namespace ActivityManagement.IocConfig
{
    public static class AddCustomKendoUiExtensions
    {
        public static IServiceCollection AddCustomKendoUi(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                option.JsonSerializerOptions.PropertyNamingPolicy = null;

            });
            return services;
        }
    }
}