using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Api;
using ActivityManagement.Common.Api.Attributes;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.IocConfig.Api.Exceptions;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.UserManager;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]/")]
    [ApiVersion("1")]
    [ApiResultFilter]
    public class UserManagerController : ControllerBase
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;

        public UserManagerController(IApplicationUserManager userManager, IApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet(template: "GetUsers")]
        [DisplayName("لیست کاربران")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<DataSourceResult>> GetUsers([DataSourceRequest] DataSourceRequest request)
        {

            DataSourceResult dataResult = await _userManager.GetAllUsersWithRoles().ToDataSourceResultAsync(request);

            return Ok(dataResult);
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
            LogicResult logicResult = new LogicResult();
            try
            {
                logicResult = await _userManager.UpdateUserProfile(viewModel);

                if (logicResult.MessageType == MessageType.Success)
                {
                    return Ok(logicResult.Message.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(ex.Message);

            }



            return BadRequest(logicResult.Message.FirstOrDefault());


        }

        [HttpPost]
        [Route("ChangeUserPhoto")]
        [JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ApiResult<string>> ChangeUserPhoto([FromForm] IFormFile file)
        {

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                LogicResult logicResult = await _userManager.UploadUserImage(file, userId);

                if (logicResult.MessageType == MessageType.Success)
                {
                    string imageUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase.Value}/wwwroot/Users/{logicResult.Message.First()}";
                    return Ok(imageUrl);
                }

                return BadRequest(logicResult.Message.FirstOrDefault());
            }

            return BadRequest(NotificationMessages.UserNotFound);


        }
        [HttpPost("CreateUser")]
        public async Task<ApiResult<string>> CreateUser([FromBody]UserViewModelApi viewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    IdentityResult result;
                    AppUser user = new AppUser();

                    if (!string.IsNullOrWhiteSpace(viewModel.PersianBirthDate))
                        viewModel.BirthDate = viewModel.PersianBirthDate.ConvertPersianToGeorgian();


                    user.EmailConfirmed = true;
                    user.UserName = viewModel.UserName;
                    user.FirstName = viewModel.FirstName;
                    user.LastName = viewModel.LastName;
                    user.PasswordHash = viewModel.Password;
                    user.Email = viewModel.Email;
                    user.BirthDate = viewModel.BirthDate;
                    user.PhoneNumber = viewModel.PhoneNumber;
                    user.IsActive = true;
                    //user.Image = viewModel.File != null ? viewModel.File.FileName : "";
                    if (viewModel.Gender != null) user.Gender = viewModel.Gender.Value;

                    result = await _userManager.CreateAsync(user, viewModel.Password);
                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByIdAsync(viewModel.RoleId.ToString());
                        if (role != null)
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                    }


                    //if (result.Succeeded)
                    //{
                    //    logicResult.MessageType = MessageType.Success;

                    //    logicResult.Message.Add(NotificationMessages.CreateSuccess);

                    //}


                    //else
                    //{
                    //    logicResult.MessageType = MessageType.Error;
                    //    logicResult.Message.Add(result.DumpErrors());
                    //}



                }
                else
                {
                    throw new AppException(ModelState.GetErrorsModelState());
                }
            }
            catch (Exception e)
            {

                throw new AppException(e.Message);

            }

            return Ok(NotificationMessages.CreateSuccess);
        }

        [HttpPost("EditUser")]
        public async Task<ApiResult<string>> EditUser([FromBody]UserViewModelApi viewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    IdentityResult result;
                    AppUser user = new AppUser();
                    viewModel.BirthDate = viewModel.PersianBirthDate.ConvertPersianToGeorgian();


                    user = await _userManager.FindByIdAsync(viewModel.Id.ToString());

                    var userRoles = await _userManager.GetRolesAsync(user);
                    result = await _userManager.RemoveFromRolesAsync(user, userRoles);
                    if (result.Succeeded)
                    {
                        user.UserName = viewModel.UserName;
                        user.FirstName = viewModel.FirstName;
                        user.LastName = viewModel.LastName;
                        user.BirthDate = viewModel.BirthDate;
                        user.Email = viewModel.Email;
                        user.PhoneNumber = viewModel.PhoneNumber;
                        user.IsActive = viewModel.IsActive;
                        if (viewModel.Gender != null) user.Gender = viewModel.Gender.Value;

                        result = await _userManager.UpdateAsync(user);

                        await _userManager.UpdateSecurityStampAsync(user);

                        
                    }


                }
                else
                {
                    throw new AppException(ModelState.GetErrorsModelState());
                }
            }
            catch (Exception e)
            {

                throw new AppException(e.Message);

            }

            return Ok(NotificationMessages.CreateSuccess);
        }

        [HttpPost("DeleteUser")]
        public async Task<ApiResult<string>> DeleteUser([FromBody]UserViewModelApi viewModel)
        {

            try
            {

                var user = await _userManager.FindByIdAsync(viewModel.Id.ToString());
                if (user == null)
                {

                    throw new AppException(NotificationMessages.UserNotFound);

                }

                else
                {
                    await _userManager.DeleteAsync(user);


                }




            }
            catch (Exception e)
            {

                throw new AppException(e.Message);

            }

            return Ok(NotificationMessages.OperationSuccess);
        }


    }
}