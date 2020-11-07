using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Api;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.IocConfig.Api.Exceptions;
using ActivityManagement.Services.EfInterfaces.Api;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.Api.RefreshToken;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.SiteSettings;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace ActivityManagement.Services.EfServices.Api
{
    public class JwtService : IjwtService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IRefreshTokenService _refreshTokenService;
        public readonly IApplicationUserManager _userManager;
        public readonly IApplicationRoleManager _roleManager;
        public readonly SiteSettings _siteSettings;

        public JwtService(IApplicationUserManager userManager,
            IApplicationRoleManager roleManager, IOptionsSnapshot<SiteSettings> siteSettings,
            IRefreshTokenService refreshTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _siteSettings = siteSettings.Value;
            _refreshTokenService = refreshTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GenerateAccessTokenAsync(AppUser user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.EncryptKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

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
        private async Task<IEnumerable<Claim>> GetClaimsAsync(AppUser user)
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
        private async Task<ResponseTokenViewModel> GenerateAccessAndRefreshToken(RequestTokenViewModel requestToken, string ipAddress)
        {
            ResponseTokenViewModel responseToken = new ResponseTokenViewModel();
            AppUser appUser = await _userManager.FindUserWithRolesByNameAsync(requestToken.UserName);

            if (appUser != null && !string.IsNullOrWhiteSpace(requestToken.RefreshToken))
            {
                RefreshToken oldRefreshToken = await _refreshTokenService.OldRefreshToken(_siteSettings.RefreshTokenSetting.ClientId, requestToken.RefreshToken, ipAddress);

                if (oldRefreshToken != null)
                {
                    AppUser userToken = await _userManager.FindByIdAsync(oldRefreshToken.UserId.ToString());
                    if (userToken == null)
                    {
                        throw new AppException(ApiResultStatusCode.LogOut, NotificationMessages.UnAuthorize, HttpStatusCode.Unauthorized);

                    }
                    if (oldRefreshToken.ExpireDate < DateTime.Now)
                    {

                        throw new AppException(ApiResultStatusCode.LogOut, NotificationMessages.UnAuthorize, HttpStatusCode.Unauthorized);
                    }

                    else
                    {
                        responseToken.RefreshToken = oldRefreshToken.Value;
                        responseToken.AccessToken = await GenerateAccessTokenAsync(appUser);
                    }



                }
                else
                {
                    throw new AppException(ApiResultStatusCode.LogOut, NotificationMessages.UserNotFound, HttpStatusCode.Unauthorized);

                }


            }
            else
            {
                throw new AppException(ApiResultStatusCode.LogOut, NotificationMessages.UserNotFound, HttpStatusCode.Unauthorized);
            }

            return responseToken;
        }


        public async Task<ResponseTokenViewModel> AuthenticateUser(HttpRequest request, RequestTokenViewModel requestToken)
        {

            string ipAddress = _httpContextAccessor.HttpContext.Connection?.RemoteIpAddress.ToString();

            ResponseTokenViewModel responseTokenViewModel = new ResponseTokenViewModel();
            if (requestToken.GrantType == "Password")
            {
                AppUser user = await _userManager.FindUserWithRolesByNameAsync(requestToken.UserName);
                if (user == null)
                {
                    responseTokenViewModel.IsSuccess = false;
                    responseTokenViewModel.Message = NotificationMessages.UserNotFound;
                    responseTokenViewModel.ApiStatusCode = ApiResultStatusCode.UnAuthorized;

                    //throw new AppException(ApiResultStatusCode.UnAuthorized, NotificationMessages.UserNotFound, HttpStatusCode.BadRequest);

                }
                else
                {
                    bool result = await _userManager.CheckPasswordAsync(user, requestToken.Password);
                    if (!result)
                    {
                        responseTokenViewModel.IsSuccess = false;
                        responseTokenViewModel.Message = NotificationMessages.InvalidUserNameOrPassword;
                        responseTokenViewModel.ApiStatusCode = ApiResultStatusCode.UnAuthorized;
                        // throw new AppException(ApiResultStatusCode.UnAuthorized, NotificationMessages.InvalidUserNameOrPassword, HttpStatusCode.BadRequest);


                    }
                    else
                    {
                        UserViewModelApi userViewModel = await _userManager.FindUserApiByIdAsync(user.Id);
                        userViewModel.Image = $"{request.Scheme}://{request.Host}{request.PathBase.Value}/wwwroot/Users/{userViewModel.Image}";

                        RefreshToken oldRefreshToken = await _refreshTokenService.GetRefreshTokenByUserIdAsync(user.Id);
                        if (oldRefreshToken != null)
                        {
                            await _refreshTokenService.RemoveRefreshTokenAsync(oldRefreshToken);

                        }

                        RefreshToken refreshToken = _refreshTokenService.CreateRefreshToken(_siteSettings.RefreshTokenSetting, user.Id, requestToken.IsRemember, ipAddress);
                        await _refreshTokenService.AddRefreshTokenAsync(refreshToken);

                        responseTokenViewModel.AccessToken = await GenerateAccessTokenAsync(user);
                        responseTokenViewModel.RefreshToken = refreshToken.Value;
                        responseTokenViewModel.User = userViewModel;
                        responseTokenViewModel.IsSuccess = true;
                    }


                }


            }
            else if (requestToken.GrantType == "RefreshToken")
            {

                responseTokenViewModel = await GenerateAccessAndRefreshToken(requestToken, ipAddress);

            }
            else
            {
                responseTokenViewModel.IsSuccess = false;
                responseTokenViewModel.ApiStatusCode = ApiResultStatusCode.NotFound;
                responseTokenViewModel.Message = NotificationMessages.UserNotFound;
                //throw new AppException(ApiResultStatusCode.BadRequest, NotificationMessages.TargetNotFounded, HttpStatusCode.BadRequest);
            }
            return responseTokenViewModel;
        }


    }
}
