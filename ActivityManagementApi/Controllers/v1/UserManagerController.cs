using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Api;
using ActivityManagement.Common.Api.Attributes;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]/")]
    [ApiVersion("1")]
    [ApiResultFilter]
    public class UserManagerController : ControllerBase
    {
        private readonly IApplicationUserManager _userManager;

        public UserManagerController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetUsers")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<List<UsersViewModel>>> GetUsers()
        {
            List<UsersViewModel> users = await _userManager.GetAllUsersWithRolesAsync();
            return Ok(users);
        }


        //[HttpGet("{id}")]
        [HttpGet()]
        [Route("GetUserLoggedIn")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<UserViewModelApi>> GetUserLoggedIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                UserViewModelApi user = await _userManager.FindUserApiByIdAsync(User.Identity.GetUserId<int>());
                return Ok(user);
            }

            return BadRequest(NotificationMessages.UserNotFound);



        }
        [HttpPost]
        [Route("UpdateUserProfile")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<string>> UpdateUserProfile([FromBody] UserViewModelApi viewModel)
        {

            LogicResult logicResult = await _userManager.UpdateUserProfile(viewModel);

            if (logicResult.MessageType == MessageType.Success)
            {
                return Ok(logicResult.Message.FirstOrDefault());
            }

            return BadRequest(logicResult.Message.FirstOrDefault());
             

        }
    
        [HttpPost]
        [Route("ChangeUserPhoto")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<string>> ChangeUserPhoto([FromForm] IFormFile file)
        {
            HttpRequest request = Request;
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                LogicResult logicResult = await _userManager.UploadUserImage(file, userId);

                if (logicResult.MessageType == MessageType.Success)
                {
                    string imageUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase.Value}/Users/{logicResult.Message.First()}";
                    return Ok(imageUrl);
                }

                return BadRequest(logicResult.Message.FirstOrDefault());
            }

            return BadRequest(NotificationMessages.UserNotFound);


        }

    }
}