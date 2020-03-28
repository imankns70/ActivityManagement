using System;
using ActivityManagement.DataLayer.Context;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfServices.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityManagement.IocConfig
{ 
    public static class AddIdentityOptionsExtentions
    {
        public static IServiceCollection AddIdentityOptions(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(
                options =>
                {
                    //Configure Password
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;

                    options.SignIn.RequireConfirmedEmail = true;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.AllowedForNewUsers = false;

                })
             .AddEntityFrameworkStores<ActivityManagementContext>()
             .AddErrorDescriber<ApplicationIdentityErrorDescriber>()
             .AddDefaultTokenProviders();


            return services;
        }
    }
}
