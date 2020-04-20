using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.DataLayer.Context;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ActivityManagement.Services.EfServices.Identity
{

    public class IdentityDbInitializer : IIdentityDbInitializer
    {
        private readonly IOptionsSnapshot<SiteSettings> _adminUserSeedOptions;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly ILogger<IdentityDbInitializer> _logger;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IServiceScopeFactory _scopeFactory;
        public readonly IMvcActionsDiscoveryService _mvcActionsDiscovery;


        public IdentityDbInitializer(
            IApplicationUserManager applicationUserManager,
            IServiceScopeFactory scopeFactory,
            IApplicationRoleManager roleManager,
            IOptionsSnapshot<SiteSettings> adminUserSeedOptions,
            ILogger<IdentityDbInitializer> logger,
            IMvcActionsDiscoveryService mvcActionsDiscovery
            )
        {
            _applicationUserManager = applicationUserManager;
            _scopeFactory = scopeFactory;
            _roleManager = roleManager;
            _adminUserSeedOptions = adminUserSeedOptions;
            _logger = logger;
            _mvcActionsDiscovery = mvcActionsDiscovery;
        }

        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ActivityManagementContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        /// <summary>
        /// Adds some default values to the IdentityDb
        /// </summary>
        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                var identityDbSeedData = serviceScope.ServiceProvider.GetService<IIdentityDbInitializer>();
                var result = identityDbSeedData.SeedDatabaseWithAdminUserAsync().Result;
                if (result == IdentityResult.Failed())
                {
                    throw new InvalidOperationException(result.DumpErrors());
                }
            }
        }

        public async Task<IdentityResult> SeedDatabaseWithAdminUserAsync()
        {
            var adminUserSeed = _adminUserSeedOptions.Value.AdminUserSeed;

            var name = adminUserSeed.Username;
            var password = adminUserSeed.Password;
            var email = adminUserSeed.Email;
            var roleName = adminUserSeed.RoleName;
            var firstName = adminUserSeed.FirstName;
            var lastName = adminUserSeed.LastName;

            var thisMethodName = nameof(SeedDatabaseWithAdminUserAsync);

            var adminUser = await _applicationUserManager.FindByNameAsync(name);
            if (adminUser != null)
            {
                _logger.LogInformation($"{thisMethodName}: adminUser already exists.");
                return IdentityResult.Success;
            }

            //Create the `Admin` Role if it does not exist
            var adminRole = await _roleManager.FindByNameAsync(roleName);
            if (adminRole == null)
            {
                adminRole = new AppRole(roleName);
                var adminRoleResult = await _roleManager.CreateAsync(adminRole);
                if (adminRoleResult == IdentityResult.Failed())
                {
                    _logger.LogError($"{thisMethodName}: adminRole CreateAsync failed. {adminRoleResult.DumpErrors()}");
                    return IdentityResult.Failed();
                }
            }
            else
            {
                _logger.LogInformation($"{thisMethodName}: adminRole already exists.");
            }



            adminUser = new AppUser
            {
                UserName = name,
                Email = email,
                EmailConfirmed = true,
                LockoutEnabled = true,
                RegisterDateTime = DateTime.Now,
                FirstName = firstName,
                LastName = lastName,
                //Gender=null,
                IsActive = true
            };
            var adminUserResult = await _applicationUserManager.CreateAsync(adminUser, password);
            if (adminUserResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: adminUser CreateAsync failed. {adminUserResult.DumpErrors()}");
                return IdentityResult.Failed();
            }

            var setLockoutResult = await _applicationUserManager.SetLockoutEnabledAsync(adminUser, enabled: false);
            if (setLockoutResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: adminUser SetLockoutEnabledAsync failed.");
                return IdentityResult.Failed();
            }

            var addToRoleResult = await _applicationUserManager.AddToRoleAsync(adminUser, adminRole.Name);
            if (addToRoleResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: adminUser AddToRoleAsync failed. {addToRoleResult.DumpErrors()}");
                return IdentityResult.Failed();
            }

            ICollection<ControllerViewModel> securedControllerActions =
                _mvcActionsDiscovery.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            IList<string> allSecuredActions = securedControllerActions.SelectMany(s => s.MvcActions).ToList().Select(a => a.ActionId).ToList();

          await _roleManager.AddOrUpdateClaimsAsync(adminRole.Id, ConstantPolicies.DynamicPermissionClaimType,allSecuredActions);

            return IdentityResult.Success;
        }
    }
}