using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiResultFilter]
    public class UserManagerController : ControllerBase
    {
        private readonly IApplicationUserManager _userManager;

        public UserManagerController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("GetUsers")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<List<UsersViewModel>>> GetUsers()
        {
            List<UsersViewModel> users = await _userManager.GetAllUsersWithRolesAsync();
            return Ok(users);
        }


        [HttpGet("{id}")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<UsersViewModel>> GetUser(int id)
        {
            int userId = User.Identity.GetUserId<int>();
            if (userId != id)
            {
                return BadRequest(NotificationMessages.UserNotFound);
            }
            UsersViewModel user = await _userManager.FindUserWithRolesByIdAsync(id);
            return Ok(user);
        }

    }
}