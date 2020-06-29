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

        [HttpGet()]
        [Route("GetUsers")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<List<UsersViewModel>>> GetUsers()
        {
            List<UsersViewModel> users = await _userManager.GetAllUsersWithRolesAsync();
            return Ok(users);
        }


        [HttpGet("{id}")]
        [Route("GetUserLoggedIn")]
        public async Task<ApiResult<UsersViewModel>> GetUserLoggedIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                UsersViewModel user = await _userManager.FindUserWithRolesByIdAsync(User.Identity.GetUserId<int>());
                return Ok(user);
            }

            return BadRequest(NotificationMessages.UserNotFound);





        }

    }
}