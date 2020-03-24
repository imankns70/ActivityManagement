using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Api.Attributes;
using ActivityManagement.Common.Attributes;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.Home;
using ActivityManagement.ViewModels.SiteSettings;
using ActivityManagement.ViewModels.UserManager;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.Controllers
{

    public class UserManagerController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;

        private readonly IWebHostEnvironment _env;


        public UserManagerController(IApplicationUserManager userManager, IApplicationRoleManager roleManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));

            _env = env;
            _env.CheckArgumentIsNull(nameof(_env));
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }


        public async Task<JsonResult> GetUsers([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult resultAsync = await _userManager.GetAllUsersWithRoles().ToDataSourceResultAsync(request);

            return Json(resultAsync);
        }

        [HttpGet, AjaxOnly]
        public IActionResult RenderCreate()
        {

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        //[JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Create(UsersViewModel viewModel)
        {
            ReturnJson returnJson = new ReturnJson();


            try
            {
                if (ModelState.IsValid)
                {

                    IdentityResult result;
                    AppUser user = new AppUser();

                    viewModel.BirthDate = viewModel.PersianBirthDate.ConvertPersianToGeorgian();


                    if (viewModel.ImageFile != null)
                    {
                        string path = Path.Combine($"{_env.WebRootPath}/Users/{ viewModel.ImageFile.FileName}");
                        FileExtensions.UploadFileResult fileResult = await viewModel.ImageFile.UploadFileAsync(FileExtensions.FileType.Image, path);
                        if (fileResult.IsSuccess == false)
                        {
                            returnJson.MessageType = MessageType.Error;
                            returnJson.Message.AddRange(fileResult.Errors);
                            return Json(returnJson);
                        }


                    }

                    user.EmailConfirmed = true;
                    user.UserName = viewModel.UserName;
                    user.FirstName = viewModel.FirstName;
                    user.LastName = viewModel.LastName;
                    user.PasswordHash = viewModel.Password;
                    user.Email = viewModel.Email;
                    user.BirthDate = viewModel.BirthDate;
                    user.PhoneNumber = viewModel.PhoneNumber;
                    user.IsActive = true;
                    user.Image = viewModel.ImageFile != null ? viewModel.ImageFile.FileName : "";
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


                    if (result.Succeeded)
                    {
                        returnJson.MessageType = MessageType.Success;
                        returnJson.Script = "UserGridRefresh()";
                        returnJson.Message.Add(InsertSuccess);

                    }


                    else
                    {
                        returnJson.MessageType = MessageType.Error;
                        returnJson.Message.Add(result.DumpErrors());
                    }



                }
                else
                {
                    returnJson.MessageType = MessageType.Error;
                    returnJson.Message = ModelState.GetErrorsModelState();
                }
            }
            catch (Exception e)
            {
                returnJson.MessageType = MessageType.Error;
                returnJson.Message.Add(e.Message);
            }




            return Json(returnJson);
        }


        [HttpGet]
        public async Task<IActionResult> RenderEdit(int id)
        {
            UsersViewModel usersViewModel = await _userManager.FindUserWithRolesByIdAsync(id);
            usersViewModel.PersianBirthDate = usersViewModel.BirthDate.ConvertGeorgianToPersian("yyyy/MM/dd");


            return PartialView(usersViewModel);
        }
        [HttpPost, AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersViewModel viewModel)
        {
            ReturnJson returnJson = new ReturnJson();

            try
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("ImageFile");
                if (ModelState.IsValid)
                {
                    IdentityResult result;
                    AppUser user = new AppUser();

                    viewModel.BirthDate = viewModel.PersianBirthDate.ConvertPersianToGeorgian();


                    user = await _userManager.FindByIdAsync(viewModel.Id.ToString());

                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (viewModel.ImageFile != null)
                    {
                        viewModel.Image = _userManager.CheckAvatarFileName(viewModel.ImageFile.FileName);
                        string path = Path.Combine($"{_env.WebRootPath}/Users/{ viewModel.Image}");
                        FileExtensions.UploadFileResult fileResult = await viewModel.ImageFile.UploadFileAsync(FileExtensions.FileType.Image, path);
                        if (fileResult.IsSuccess == false)
                        {
                            returnJson.MessageType = MessageType.Error;
                            returnJson.Message.AddRange(fileResult.Errors);
                            return Json(returnJson);
                        }

                        FileExtensions.DeleteFile($"{_env.WebRootPath}/Users/{user.Image}");
                        user.Image = viewModel.Image;
                    }

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

                        var role = await _roleManager.FindByIdAsync(viewModel.RoleId.ToString());

                        if (role != null)
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                    }



                    if (result.Succeeded)
                    {
                        returnJson.MessageType = MessageType.Success;
                        returnJson.Script = "UserGridRefresh()";
                        returnJson.Message.Add(InsertSuccess);

                    }


                    else
                    {
                        returnJson.MessageType = MessageType.Error;
                        returnJson.Message.Add(result.DumpErrors());
                    }


                }
                else
                {
                    returnJson.MessageType = MessageType.Error;
                    returnJson.Message = ModelState.GetErrorsModelState();
                }
            }
            catch (Exception e)
            {
                returnJson.MessageType = MessageType.Error;
                returnJson.Message.Add(e.Message);
            }


            return Json(returnJson);
        }

        //[HttpGet, DisplayName("نمایش حذف کاربر")]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RenderDelete(int id)
        {
            UsersViewModel usersViewModel = await _userManager.FindUserWithRolesByIdAsync(id);
            return PartialView(usersViewModel);
        }
        [HttpPost,AjaxOnly]
        [ValidateAntiForgeryToken]
        //[DisplayName("ارسال حذف کاربر")]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Delete(UsersViewModel viewModel)
        {
            ReturnJson returnJson = new ReturnJson();
            var user = await _userManager.FindByIdAsync(viewModel.Id.ToString());
            if (user == null)
            {
                returnJson.MessageType = MessageType.Error;
                returnJson.Message.Add(UserNotFound);
            }

            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    FileExtensions.DeleteFile($"{_env.WebRootPath}/Users/{user.Image}");
                    returnJson.MessageType = MessageType.Success;
                    returnJson.Script = "UserGridRefresh()";
                    returnJson.Message.Add(OperationSuccess);
                }
                else
                {
                    returnJson.MessageType = MessageType.Error;
                    returnJson.Message.Add(result.DumpErrors());
                }

            }

            return Json(returnJson);
        }

        public async Task<IActionResult> RenderDetail(int id)
        {
            UsersViewModel usersViewModel = await _userManager.FindUserWithDetailIdAsync(id);

            return PartialView(usersViewModel);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        /// <summary>
        /// نمایش صفحه بازنشانی کلمه عبور
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ResetPassword(int userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            
            var viewModel = new ResetPasswordViewModel
            {
                UserId = userId,
                UserName = user.UserName
            };

            return PartialView(viewModel);
        }
        /// <summary>
        /// انجام عملیات بازنشانی کلمه عبور
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {

            ReturnJson returnJson= new ReturnJson();
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser user = await _userManager.FindByIdAsync(viewModel.UserId.ToString());
                    if (user == null)
                    {
                        returnJson.MessageType = MessageType.Error;
                        returnJson.Message.Add(UserNotFound);
                    }
                    else
                    {
                        await _userManager.RemovePasswordAsync(user);
                        var result = await _userManager.AddPasswordAsync(user, viewModel.NewPassword);
                        if (result.Succeeded)
                        {
                            returnJson.MessageType = MessageType.Success;
                            returnJson.Script = "UserGridRefresh()";
                            returnJson.Message.Add("بازنشانی کلمه عبور با موفقیت انجام شد.");
                        }
                        else
                        {
                            returnJson.MessageType = MessageType.Error;
                            returnJson.Message.Add(result.DumpErrors());
                        }
                          
                        
                    }

                  
                }
            }
            catch (Exception e)
            {
                returnJson.MessageType = MessageType.Error;
                returnJson.Message.Add(e.Message);
            }
          

            return Json(returnJson);
        }
    }
}