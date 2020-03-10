using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Api.Attributes;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.Services.EfInterfaces.Identity;
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
        private const string UserNotFound = "کاربر یافت نشد.";

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

        [HttpGet]
        public async Task<JsonResult> GetUsers([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult resultAsync = await _userManager.GetAllUsersWithRoles().ToDataSourceResultAsync(request);
            
            return Json(resultAsync);
        }

        //[HttpGet]
        //public async Task<IActionResult> RenderUser(int? userId)
        //{
        //    UsersViewModel usersViewModel = new UsersViewModel();
        //    usersViewModel.AllRoles = _roleManager.GetAllRoles();

        //    if (userId != null)
        //    {
        //        usersViewModel = await _userManager.FindUserWithRolesByIdAsync((int)userId);
        //        usersViewModel.PersianBirthDate = usersViewModel.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd");
        //    }

        //    return PartialView("_RenderUser", usersViewModel);
        //}

        //[HttpPost]
        ////[Authorize]
        //[JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        //public async Task<IActionResult> CreateOrUpdate(UsersViewModel viewModel)
        //{
        //    viewModel.AllRoles = _roleManager.GetAllRoles();
        //    if (viewModel.Id != null)
        //    {
        //        ModelState.Remove("Password");
        //        ModelState.Remove("ConfirmPassword");
        //        ModelState.Remove("ImageFile");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        IdentityResult result;
        //        AppUser user = new AppUser();

        //        viewModel.BirthDate = viewModel.PersianBirthDate.ConvertShamsiToMiladi();

        //        if (viewModel.Id != null)
        //        {
        //            user = await _userManager.FindByIdAsync(viewModel.Id.ToString());

        //            var userRoles = await _userManager.GetRolesAsync(user);
        //            if (viewModel.ImageFile != null)
        //            {
        //                viewModel.Image = _userManager.CheckAvatarFileName(viewModel.ImageFile.FileName);
        //                FileExtensions.UploadFileResult fileResult = await viewModel.ImageFile.UploadFileAsync(FileExtensions.FileType.Image, $"{_env.WebRootPath}/images/avatars/{viewModel.Image}");
        //                if (fileResult.IsSuccess == false)
        //                {
        //                    ModelState.AddModelError(string.Empty, InvalidImage);
        //                    return PartialView("_RenderUser", viewModel);
        //                }

        //                FileExtensions.DeleteFile($"{_env.WebRootPath}/avatars/{user.Image}");
        //                user.Image = viewModel.Image;
        //            }

        //            result = await _userManager.RemoveFromRolesAsync(user, userRoles);
        //            if (result.Succeeded)
        //            {
        //                user.UserName = viewModel.UserName;
        //                user.FirstName = viewModel.FirstName;
        //                user.LastName = viewModel.LastName;
        //                user.BirthDate = viewModel.BirthDate;
        //                //user.Bio = viewModel.Bio;
        //                user.Email = viewModel.Email;
        //                user.PhoneNumber = viewModel.PhoneNumber;

        //                if (viewModel.Gender != null) user.Gender = viewModel.Gender.Value;
        //                result = await _userManager.UpdateAsync(user);
        //                await _userManager.UpdateSecurityStampAsync(user);
        //                var role = await _roleManager.FindByIdAsync(viewModel.RoleId.ToString());
        //                if (role != null)
        //                {
        //                    await _userManager.AddToRoleAsync(user, role.Name);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (viewModel.ImageFile != null)
        //            {
        //                FileExtensions.UploadFileResult fileResult = await viewModel.ImageFile.UploadFileAsync(FileExtensions.FileType.Image, $"{_env.WebRootPath}/avatars/{viewModel.Image}");
        //                if (fileResult.IsSuccess == false)
        //                    ModelState.AddModelError(string.Empty, InvalidImage);
        //            }

        //            user.EmailConfirmed = true;
        //            user.UserName = viewModel.UserName;
        //            user.FirstName = viewModel.FirstName;
        //            user.LastName = viewModel.LastName;
        //            user.PasswordHash = viewModel.Password;
        //            user.Email = viewModel.Email;
        //            user.BirthDate = viewModel.BirthDate;
        //            user.PhoneNumber = viewModel.PhoneNumber;
        //            //user.Bio = viewModel.Bio;
        //            user.Image = viewModel.Image;
        //            if (viewModel.Gender != null) user.Gender = viewModel.Gender.Value;

        //            result = await _userManager.CreateAsync(user, viewModel.Password);
        //            if (result.Succeeded)
        //            {
        //                var role = await _roleManager.FindByIdAsync(viewModel.RoleId.ToString());
        //                if (role != null)
        //                {
        //                    await _userManager.AddToRoleAsync(user, role.Name);
        //                }
        //            }

        //        }

        //        if (result.Succeeded)
        //            TempData["notification"] = OperationSuccess;

        //        else
        //            ModelState.AddErrorsFromResult(result);
        //    }

        //    return PartialView("_RenderUser", viewModel);
        //}

       

       
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}