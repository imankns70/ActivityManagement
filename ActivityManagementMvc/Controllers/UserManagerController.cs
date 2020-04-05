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
            LogicResult logicResult = new LogicResult();


            try
            {
                if (ModelState.IsValid)
                {

                    IdentityResult result;
                    AppUser user = new AppUser();

                    viewModel.BirthDate = viewModel.PersianBirthDate.ConvertPersianToGeorgian();


                    if (viewModel.ImageFile != null)
                    {
                        viewModel.Image = _userManager.CheckAvatarFileName(viewModel.ImageFile.FileName);
                        string path = Path.Combine($"{_env.WebRootPath}/Users/{ viewModel.Image}");
                        FileExtensions.UploadFileResult fileResult = await viewModel.ImageFile.UploadFileAsync(FileExtensions.FileType.Image, path);
                        if (fileResult.IsSuccess == false)
                        {
                            logicResult.MessageType = MessageType.Error;
                            logicResult.Message.AddRange(fileResult.Errors);
                            return Json(logicResult);
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
                        logicResult.MessageType = MessageType.Success;
                        logicResult.Script = "UserGridRefresh()";
                        logicResult.Message.Add(NotificationMessages.CreateSuccess);

                    }


                    else
                    {
                        logicResult.MessageType = MessageType.Error;
                        logicResult.Message.Add(result.DumpErrors());
                    }



                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message = ModelState.GetErrorsModelState();
                }
            }
            catch (Exception e)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(e.Message);
            }




            return Json(logicResult);
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
            LogicResult logicResult = new LogicResult();

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
                            logicResult.MessageType = MessageType.Error;
                            logicResult.Message.AddRange(fileResult.Errors);
                            return Json(logicResult);
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
                        logicResult.MessageType = MessageType.Success;
                        logicResult.Script = "UserGridRefresh()";
                        logicResult.Message.Add(NotificationMessages.CreateSuccess);

                    }


                    else
                    {
                        logicResult.MessageType = MessageType.Error;
                        logicResult.Message.Add(result.DumpErrors());
                    }


                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message = ModelState.GetErrorsModelState();
                }
            }
            catch (Exception e)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(e.Message);
            }


            return Json(logicResult);
        }

        //[HttpGet, DisplayName("نمایش حذف کاربر")]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RenderDelete(int id)
        {
            UsersViewModel usersViewModel = await _userManager.FindUserWithRolesByIdAsync(id);
            return PartialView(usersViewModel);
        }
        [HttpPost, AjaxOnly]
        [ValidateAntiForgeryToken]
        //[DisplayName("ارسال حذف کاربر")]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Delete(UsersViewModel viewModel)
        {
            LogicResult logicResult = new LogicResult();
            var user = await _userManager.FindByIdAsync(viewModel.Id.ToString());
            if (user == null)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.UserNotFound);
            }

            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    FileExtensions.DeleteFile($"{_env.WebRootPath}/Users/{user.Image}");
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Script = "UserGridRefresh()";
                    logicResult.Message.Add(NotificationMessages.OperationSuccess);
                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(result.DumpErrors());
                }

            }

            return Json(logicResult);
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
        public async Task<IActionResult> RenderResetPassword(int userId)
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

            LogicResult logicResult = new LogicResult();
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser user = await _userManager.FindByIdAsync(viewModel.UserId.ToString());
                    if (user == null)
                    {
                        logicResult.MessageType = MessageType.Error;
                        logicResult.Message.Add(NotificationMessages.UserNotFound);
                    }
                    else
                    {


                        await _userManager.RemovePasswordAsync(user);
                        var result = await _userManager.AddPasswordAsync(user, viewModel.NewPassword);
                        if (result.Succeeded)
                        {
                            logicResult.MessageType = MessageType.Success;
                            logicResult.Script = "UserGridRefresh()";
                            logicResult.Message.Add("بازنشانی کلمه عبور با موفقیت انجام شد.");
                        }
                        else
                        {
                            logicResult.MessageType = MessageType.Error;
                            logicResult.Message.Add(result.DumpErrors());
                        }


                    }

                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message = ModelState.GetErrorsModelState();
                }
            }
            catch (Exception e)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(e.Message);
            }


            return Json(logicResult);
        }

        /// <summary>
        /// قفل و خروج از حالت قفل حساب کاربر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LockOrUnLockUserAccount(int id)
        {
            LogicResult logicResult = new LogicResult();
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.UserNotFound);
            }

            else
            {
                if (user.LockoutEnd == null)
                {

                    user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(20);
                }

                else
                {
                    if (user.LockoutEnd > DateTime.Now)
                    {

                        user.LockoutEnd = null;
                    }
                    else
                    {

                        user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(20);
                    }
                }

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Script = "UserGridRefresh()";

                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(result.DumpErrors());
                }

            }


            return Json(logicResult);
        }

        /// <summary>
        /// فعال و غیر فعال کردن فقل حساب کاربر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ChangeLockOutEnable(int id)
        {
            LogicResult logicResult = new LogicResult();
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.UserNotFound);
            }

            else
            {
                if (user.LockoutEnabled)
                {
                    user.LockoutEnabled = false;
                }

                else
                {
                    user.LockoutEnabled = true;
                }


            }
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                logicResult.MessageType = MessageType.Success;
                logicResult.Script = "UserGridRefresh()";

            }
            else
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(result.DumpErrors());
            }
            return Json(logicResult);
        }

        /// <summary>
        /// فعال و غیر فعال کردن کاربر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> InActiveOrActiveUser(int id)
        {
            LogicResult logicResult = new LogicResult();

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.UserNotFound);
            }
            else
            {

                if (user.IsActive)
                {
                    user.IsActive = false;
                }

                else
                {
                    user.IsActive = true;
                }

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Script = "UserGridRefresh()";

                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(result.DumpErrors());
                }
            }
            return Json(logicResult);

        }

        /// <summary>
        /// تایید و عدم تایید وضعیت ایمیل کاربر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ChangeEmailConfirmed(int userId)
        {
            LogicResult logicResult = new LogicResult();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.UserNotFound);
            }
            else
            {


                if (user.EmailConfirmed)
                {
                    user.EmailConfirmed = false;
                }

                else
                {
                    user.EmailConfirmed = true;
                }


                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Script = "UserGridRefresh()";

                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(result.DumpErrors());
                }
            }
            return Json(logicResult);
        }

        /// <summary>
        /// تایید و عدم تایید وضعیت شماره موبایل کاربر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ChangePhoneNumberConfirmed(int id)
        {
            LogicResult logicResult = new LogicResult();

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.UserNotFound);
            }
            else
            {


                if (user.PhoneNumberConfirmed)
                {
                    user.PhoneNumberConfirmed = false;
                }

                else
                {
                    user.PhoneNumberConfirmed = true;
                }
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Script = "UserGridRefresh()";

                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(result.DumpErrors());
                }
            }
            return Json(logicResult);

        }
        /// <summary>
        /// فعال و غیر فعال کردن احرازهویت دو مرحله ای
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ChangeTwoFactorEnabled(int userId)
        {
            LogicResult logicResult = new LogicResult();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.UserNotFound);
            }
            else
            {
                if (user.TwoFactorEnabled)
                {
                    user.TwoFactorEnabled = false;
                }

                else
                {
                    user.TwoFactorEnabled = true;
                }

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Script = "UserGridRefresh()";

                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(result.DumpErrors());
                }
            }

            return Json(logicResult);
        }
        [AjaxOnly]
        public IActionResult ComboUser([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult resultAsync = _userManager.GetAllUsersAsync().Result.Select(a => new { text = a.UserName, value = a.Id }).ToDataSourceResult(request);

            return Json(resultAsync);
        }
    }
}