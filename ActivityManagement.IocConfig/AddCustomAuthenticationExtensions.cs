using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ActivityManagement.Common;
using ActivityManagement.Common.Api;
using ActivityManagement.IocConfig.Api.Exceptions;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.SiteSettings;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManagement.IocConfig
{
    public static class AddCustomAuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, SiteSettings siteSettings)
        {
            services.AddAuthentication(options =>
            {

                //زمانی که بخواهیم اعتبار سنجی کاربران بر اساس توکن باشد اینها از حالت کامنت خارج می شوند
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
              {
                  var secretKey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.SecretKey);
                  var encryptionKey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.EncryptKey);

                  var validationParameters = new TokenValidationParameters
                  {
                      // token must be signed
                      RequireSignedTokens = true,

                      // validate secret key when server gets token
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                      RequireExpirationTime = true,

                      // check when token is expired so then take it down
                      ValidateLifetime = true,

                      ValidateAudience = true, //default : false
                      ValidAudience = siteSettings.JwtSettings.Audience,

                      ValidateIssuer = true, //default : false
                      ValidIssuer = siteSettings.JwtSettings.Issuer,

                      TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                  };

                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;
                  options.TokenValidationParameters = validationParameters;
                  options.Events = new JwtBearerEvents
                  {
                      OnAuthenticationFailed = context =>
                      {
                          if (context.Exception != null)
                              throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);
                          return Task.CompletedTask;
                      },

                      OnTokenValidated = async context =>
                      {
                          var userRepository = context.HttpContext.RequestServices.GetRequiredService<IApplicationUserManager>();

                          var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                          if (claimsIdentity?.Claims?.Any() != true)
                              context.Fail("This token has no claims.");

                          var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                          if (!securityStamp.HasValue())
                              context.Fail("This token has no secuirty stamp");

                          //var userId = claimsIdentity.GetUserId<string>();
                          var user = await userRepository.GetUserAsync(context.Principal);

                          if (user.SecurityStamp != securityStamp)
                              context.Fail("Token secuirty stamp is not valid.");

                          if (!user.IsActive)
                              context.Fail("User is not active.");
                      },

                      OnChallenge = context =>
                      {
                          if (context.AuthenticateFailure != null)
                              throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                          throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                      }

                  };
              });
            //.AddGoogle(options =>
            //{
            //    options.ClientId = "315654760867-d01fsd0fb847vft0fbo6hvbgqghrt5ph.apps.googleusercontent.com";
            //    options.ClientSecret = "F7rY4md1LciG24O_4J_RAPct";
            //})
            //.AddYahoo(options =>
            //{
            //    options.ClientId = "dj0yJmk9aWxnZVZNTGVwVXhWJnM9Y29uc3VtZXJzZWNyZXQmc3Y9MCZ4PWQz";
            //    options.ClientSecret = "9d68b57943e8035cd0771f49d2b54af10797eb4e";
            //});

            return services;
        }
    }
}
