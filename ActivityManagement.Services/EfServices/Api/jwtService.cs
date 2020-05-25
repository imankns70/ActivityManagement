using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Api;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ActivityManagement.Services.EfServices.Api
{
    public class JwtService : IJwtService
    {
        public readonly IApplicationUserManager UserManager;
        public readonly IApplicationRoleManager RoleManager;
        public readonly SiteSettings SiteSettings;
        public JwtService(IApplicationUserManager userManager, IApplicationRoleManager roleManager, IOptionsSnapshot<SiteSettings> siteSettings)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SiteSettings = siteSettings.Value;
        }

        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            var secretKey = Encoding.UTF8.GetBytes(SiteSettings.JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(SiteSettings.JwtSettings.EncryptKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = SiteSettings.JwtSettings.Issuer,
                Audience = SiteSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(SiteSettings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(SiteSettings.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(await GetClaimsAsync(user)),
                EncryptingCredentials = encryptingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }


        public async Task<IEnumerable<Claim>> GetClaimsAsync(AppUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                //new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                new Claim(new ClaimsIdentityOptions().SecurityStampClaimType,user.SecurityStamp),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };


            List<AppRole> roles = RoleManager.GetAllRoles();

            foreach (var item in roles)
            {
                var roleClaim = await RoleManager.FindClaimsInRole(item.Id);
                foreach (var claim in roleClaim.Claims)
                {
                    claims.Add(new Claim(ConstantPolicies.DynamicPermissionClaimType, claim.ClaimValue));
                }

                claims.Add(new Claim(ClaimTypes.Role, item.Name));
            }
            // instead of userClaim use roleClaim 
            // var userClaims = await _userManager.GetClaimsAsync(user);
            // var userRoles = await _userManager.GetRolesAsync(user);
            // foreach (var item in userClaims)
            //     Claims.Add(new Claim(ConstantPolicies.DynamicPermissionClaimType, item.Value));

            // foreach (var item in userRoles)
            //     Claims.Add(new Claim(ClaimTypes.Role, item));

            return claims;
        }
    }
}
