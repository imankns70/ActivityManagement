using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.Home;
using ActivityManagement.ViewModels.SiteSettings;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.Controllers
{
    public class UserManagerController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;

        private readonly IHostingEnvironment _env;
        private const string UserNotFound = "کاربر یافت نشد.";

        public UserManagerController(IApplicationUserManager userManager, IApplicationRoleManager roleManager, IHostingEnvironment env)
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

             BreadCrumbViewModel breadCrumbViewModel = new BreadCrumbViewModel
            {
                Title = "مدیریت کاربران",
                Url = "/UserManager/Index"

            };
            HomeViewModel homeViewModel = new HomeViewModel(breadCrumbViewModel);

            return View(homeViewModel);
        }


        public async Task<JsonResult> GetUsers(string search, string order, int offset, int limit, string sort)
        {
            List<UsersViewModel> allUsers;
            int total = _userManager.Users.Count();

            if (string.IsNullOrWhiteSpace(search))
                search = "";

            if (limit == 0)
                limit = total;

            if (sort == "نام")
            {
                if (order == "asc")
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, true, null, null, null, null, search);
                else
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, false, null, null, null, null, search);
            }

            else if (sort == "نام خانوادگی")
            {
                if (order == "asc")
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, true, null, null, null, search);
                else
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, false, null, null, null, search);
            }

            else if (sort == "ایمیل")
            {
                if (order == "asc")
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, null, true, null, null, search);
                else
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, null, false, null, null, search);
            }

            else if (sort == "نام کاربری")
            {
                if (order == "asc")
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, null, null, true, null, search);
                else
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, null, null, false, null, search);
            }

            else if (sort == "تاریخ عضویت")
            {
                if (order == "asc")
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, null, null, null, true, search);
                else
                    allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, null, null, null, false, search);
            }

            else
                allUsers = await _userManager.GetPaginateUsersAsync(offset, limit, null, null, null, null, null, search);

            if (search != "")
                total = allUsers.Count();

            return Json(new { total = total, rows = allUsers });
        }




        [HttpGet]
        public async Task<IActionResult> RenderUser(int? userId)
        {
            UsersViewModel usersViewModel = new UsersViewModel();
            usersViewModel.AllRoles = _roleManager.GetAllRoles();

            if (userId != null)
            {
                usersViewModel = await _userManager.FindUserWithRolesByIdAsync((int)userId);
                usersViewModel.PersianBirthDate = usersViewModel.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd");
            }

            return PartialView("_RenderUser", usersViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(UsersViewModel viewModel)
        {
            viewModel.AllRoles = _roleManager.GetAllRoles();
            if (viewModel.Id != null)
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("ImageFile");
            }

            if (ModelState.IsValid)
            {
                IdentityResult result;
                AppUser user = new AppUser();
              

                viewModel.BirthDate = viewModel.PersianBirthDate.ConvertShamsiToMiladi();

                if (viewModel.Id != null)
                {
                    user = await _userManager.FindByIdAsync(viewModel.Id.ToString());

                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (viewModel.ImageFile != null)
                    {
                        viewModel.Image = _userManager.CheckAvatarFileName(viewModel.ImageFile.FileName);
                        FileExtensions.UploadFileResult fileResult = await viewModel.ImageFile.UploadFileAsync(FileExtensions.FileType.Image,$"{_env.WebRootPath}/images/avatars/{viewModel.Image}");
                        if (fileResult.IsSuccess == false)
                        {
                            ModelState.AddModelError(string.Empty, InvalidImage);
                            return PartialView("_RenderUser", viewModel);
                        }


                        FileExtensions.DeleteFile($"{_env.WebRootPath}/avatars/{user.Image}");
                        user.Image = viewModel.Image;
                    }


                    result = await _userManager.RemoveFromRolesAsync(user, userRoles);
                    if (result.Succeeded)
                    {
                        user.UserName = viewModel.UserName;
                        user.FirstName = viewModel.FirstName;
                        user.LastName = viewModel.LastName;
                        user.BirthDate = viewModel.BirthDate;
                        //user.Bio = viewModel.Bio;
                        user.Email = viewModel.Email;
                        user.PhoneNumber = viewModel.PhoneNumber;

                        if (viewModel.Gender != null) user.Gender = viewModel.Gender.Value;
                        result = await _userManager.UpdateAsync(user);

                        var role = await _roleManager.FindByIdAsync(viewModel.RoleId.ToString());
                        if (role != null)
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                    }
                }

                else
                {
                    if (viewModel.ImageFile != null)
                    {
                        FileExtensions.UploadFileResult fileResult = await viewModel.ImageFile.UploadFileAsync(FileExtensions.FileType.Image,$"{_env.WebRootPath}/avatars/{viewModel.Image}");
                        if (fileResult.IsSuccess == false)
                            ModelState.AddModelError(string.Empty, InvalidImage);
                    }
                   

                    user.EmailConfirmed = true;
                    user.UserName = viewModel.UserName;
                    user.FirstName = viewModel.FirstName;
                    user.LastName = viewModel.LastName;
                    user.PasswordHash = viewModel.Password;
                    user.Email = viewModel.Email;
                    user.BirthDate = viewModel.BirthDate;
                    user.PhoneNumber = viewModel.PhoneNumber;
                    //user.Bio = viewModel.Bio;
                    user.Image = viewModel.Image;
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

                }

                if (result.Succeeded)
                    TempData["notification"] = OperationSuccess;


                else
                    ModelState.AddErrorsFromResult(result);
            }

            return PartialView("_RenderUser", viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? userId)
        {
            if (userId == null)
                ModelState.AddModelError(string.Empty, UserNotFound);
            else
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    ModelState.AddModelError(string.Empty, UserNotFound);
                else
                    return PartialView("_DeleteConfirmation", user);
            }
            return PartialView("_DeleteConfirmation");
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(AppUser model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
                ModelState.AddModelError(string.Empty, UserNotFound);
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    FileExtensions.DeleteFile($"{_env.WebRootPath}/avatars/{user.Image}");
                    TempData["notification"] = DeleteSuccess;
                    return PartialView("_DeleteConfirmation", user);
                }
                else
                    ModelState.AddErrorsFromResult(result);
            }

            return PartialView("_DeleteConfirmation");
        }



        [HttpPost, ActionName("DeleteGroup")]
        public async Task<IActionResult> DeleteGroupConfirmed(int[] btSelectItem)
        {
            if (!btSelectItem.Any())
                ModelState.AddModelError(string.Empty, "هیچ کاربری برای حذف انتخاب نشده است.");
            else
            {
                foreach (var item in btSelectItem)
                {
                    var user = await _userManager.FindByIdAsync(item.ToString());
                    var result = await _userManager.DeleteAsync(user);
                    FileExtensions.DeleteFile($"{_env.WebRootPath}/avatars/{user.Image}");
                }
                TempData["notification"] = "حذف گروهی اطلاعات با موفقیت انجام شد..";
            }

            return PartialView("_DeleteGroup");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}