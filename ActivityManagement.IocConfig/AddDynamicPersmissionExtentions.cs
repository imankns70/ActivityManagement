using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.Services.EfServices.Identity;
using ActivityManagement.ViewModels.DynamicAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityManagement.IocConfig
{
    public static class AddDynamicPermissionExtensions
    {
        public static IServiceCollection AddDynamicPermission(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();
            services.AddSingleton<ISecurityTrimmingService, SecurityTrimmingService>();


            return services;
        }
    }
}
