using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common.Api;
using ActivityManagement.Common.Api.Attributes;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiResultFilter]
    public class UserManagerController : Controller
    {
        private readonly IApplicationUserManager _userManager;

        public UserManagerController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ApiResult<List<UsersViewModel>>> Get()
        {
            List<UsersViewModel> users= await _userManager.GetAllUsersWithRolesAsync();
            return Ok(users);
        }
    }
}