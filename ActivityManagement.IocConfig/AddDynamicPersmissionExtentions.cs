using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.Services.EfServices.Identity;
using ActivityManagement.ViewModels.DynamicAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityManagement.IocConfig
{
    public static class AddDynamicPersmissionExtentions
    {
        public static IServiceCollection AddDynamicPersmission(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();
            services.AddSingleton<ISecurityTrimmingService, SecurityTrimmingService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstantPolicies.DynamicPermission, policy => policy.Requirements.Add(new DynamicPermissionRequirement()));
            });

            return services;
        }
    }
}
