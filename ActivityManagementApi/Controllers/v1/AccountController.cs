using System;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Api;
using ActivityManagement.Common.Api.Attributes;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.IocConfig.Api.Exceptions;
using ActivityManagement.Services.EfInterfaces.Api;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.Api.RefreshToken;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]/")]
    [ApiVersion("1")]
    [ApiResultFilter]
    public class AccountController : ControllerBase
    {

        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IjwtService _jwtService;


        public AccountController(IApplicationUserManager userManager, IApplicationRoleManager roleManager,
            IjwtService jwtService)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));


            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));


            _jwtService = jwtService;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

        }
        public async Task<ApiResult<ResponseTokenViewModel>> Auth(RequestTokenViewModel requestToken)
        {
            if (ModelState.IsValid)
            {
                ResponseTokenViewModel responseTokenViewModel = new ResponseTokenViewModel();
                if (requestToken.GrantType == "Password")
                {
                    AppUser user = await _userManager.FindUserWithRolesByNameAsync(requestToken.UserName);
                    if (user == null)
                    {
                        return BadRequest(NotificationMessages.UserNotFound);
                    }

                    bool result = await _userManager.CheckPasswordAsync(user, requestToken.Password);
                    if (!result)
                    {
                        return BadRequest(NotificationMessages.InvalidUserNameOrPassword);

                    }

                    UserViewModelApi userViewModel = await _userManager.FindUserApiByIdAsync(user.Id);
                    userViewModel.Image = $"{Request.Scheme}://{Request.Host}{Request.PathBase.Value}/wwwroot/Users/{userViewModel.Image}";
                    userViewModel.Token = await _jwtService.GenerateAccessTokenAsync(user);
                }
                else if (requestToken.GrantType == "RefreshToken")
                {

                }
            }
            return BadRequest(ModelState.GetErrorsModelState());
        }
        [HttpPost("SignIn")]
        public async Task<ApiResult<UserViewModelApi>> SignIn([FromBody]SignInViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindUserWithRolesByNameAsync(viewModel.UserName);
                if (user == null)
                {
                    return BadRequest(NotificationMessages.UserNotFound);
                }

                bool result = await _userManager.CheckPasswordAsync(user, viewModel.Password);
                if (!result)
                {
                    return BadRequest(NotificationMessages.InvalidUserNameOrPassword);

                }

                UserViewModelApi userViewModel = await _userManager.FindUserApiByIdAsync(user.Id);
                userViewModel.Image = $"{Request.Scheme}://{Request.Host}{Request.PathBase.Value}/wwwroot/Users/{userViewModel.Image}";
                userViewModel.Token = await _jwtService.GenerateAccessTokenAsync(user);
                return Ok(userViewModel);
            }

            return BadRequest(ModelState.GetErrorsModelState());


        }
        [HttpPost("Register")]
        public async Task<ApiResult<string>> Register([FromBody]UsersViewModel viewModel)
        {

            ModelState.Remove("roleId");
            ModelState.Remove("PersianBirthDate");
            ModelState.Remove("PhoneNumber");
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    EmailConfirmed = true,
                    UserName = viewModel.UserName,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    PasswordHash = viewModel.Password,
                    Email = viewModel.Email,
                    Gender = viewModel.Gender ?? viewModel.Gender

                };
                IdentityResult identityResult = await _userManager.CreateAsync(user, viewModel.Password);
                if (identityResult.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, "کاربر عادی");
                    return Ok(NotificationMessages.RegisterSuccess);
                }

                return BadRequest(identityResult.DumpErrors());


            }

            return BadRequest(ModelState.GetErrorsModelState());


        }
    }
}