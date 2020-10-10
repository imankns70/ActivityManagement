using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Api;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.Api.RefreshToken;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.SiteSettings;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ActivityManagement.Services.EfServices.Api
{
    public class JwtService : IJwtService
    {
        public readonly IApplicationUserManager _userManager;
        public readonly IApplicationRoleManager _roleManager;
        public readonly SiteSettings SiteSettings;
        public JwtService(IApplicationUserManager userManager, IApplicationRoleManager roleManager, IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            SiteSettings = siteSettings.Value;
        }

        public async Task<string> GenerateAccessTokenAsync(AppUser user)
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

        public async Task<string> GenerateRefreshToken(RequestTokenViewModel requestToken)
        {
            AppUser appUser = await _userManager.FindByNameAsync(requestToken.UserName);

            if (appUser != null && await _userManager.CheckPasswordAsync(appUser, requestToken.PassWord)) ;

            RefreshToken newRefreshToken = CreateRefreshToken(requestToken.ClientId, appUser.Id);
            return "";
        }


        private RefreshToken CreateRefreshToken(string ClientId, int userId)
        {
            return new RefreshToken
            {
                ClientId = ClientId,
                UserId = userId,
                Value = Guid.NewGuid().ToString("N"),
                ExpireDate = DateTime.Now.AddDays(1)
            };
        }
        public async Task<IEnumerable<Claim>> GetClaimsAsync(AppUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                //new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                new Claim(new ClaimsIdentityOptions().SecurityStampClaimType,user.SecurityStamp),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };

            claims.AddRange(await _roleManager.GetDynamicPermissionClaimsByRoleIdAsync(user.Roles.First().RoleId));
            //List<AppRole> roles = RoleManager.GetAllRolesWithClaims();

            //foreach (var item in user.Roles)
            //{
            //    var roleClaim = await _roleManager.FindClaimsInRole(item.RoleId);
            //    if (roleClaim.Claims.Any())
            //    {
            //        foreach (var claim in roleClaim.Claims)
            //        {
            //            if (claim.ClaimType == ConstantPolicies.DynamicPermission)
            //            {
            //                claims.Add(new Claim(ConstantPolicies.DynamicPermissionClaimType, claim.ClaimValue));

            //            }
            //        }
            //    }

            //    claims.Add(new Claim(ClaimTypes.Role, item.Role.Name));
            //}


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
