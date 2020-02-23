using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.Services.Api.Contract;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.SiteSettings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManagement.Services.Api
{
    public class jwtService : IjwtService
    {
        public readonly IApplicationUserManager _userManager;
        public readonly IApplicationRoleManager _roleManager;
        public readonly SiteSettings _siteSettings;
        public jwtService(IApplicationUserManager userManager, IApplicationRoleManager roleManager, IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _siteSettings = siteSettings.Value;
        }

        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encrytionKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.EncrypKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrytionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _siteSettings.JwtSettings.Issuer,
                Audience = _siteSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.ExpirationMinutes),
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
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                new Claim(new ClaimsIdentityOptions().SecurityStampClaimType,user.SecurityStamp),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };


            List<AppRole> roles = _roleManager.GetAllRoles();

            foreach (var item in roles)
            {
                var roleClaim = await _roleManager.FindClaimsInRole(item.Id);
                foreach (var claim in roleClaim.Claims)
                {
                    Claims.Add(new Claim(ConstantPolicies.DynamicPermissionClaimType, claim.ClaimValue));
                }

                Claims.Add(new Claim(ClaimTypes.Role, item.Name));
            }
            // instead of userClaim use roleClaim 
            // var userClaims = await _userManager.GetClaimsAsync(user);
            // var userRoles = await _userManager.GetRolesAsync(user);
            // foreach (var item in userClaims)
            //     Claims.Add(new Claim(ConstantPolicies.DynamicPermissionClaimType, item.Value));

            // foreach (var item in userRoles)
            //     Claims.Add(new Claim(ClaimTypes.Role, item));

            return Claims;
        }
    }
}
