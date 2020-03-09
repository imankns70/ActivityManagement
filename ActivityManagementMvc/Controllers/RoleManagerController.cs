using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Attributes;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.Home;
using ActivityManagement.ViewModels.RoleManager;
using ActivityManagement.ViewModels.SiteSettings;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.Controllers
{

    [DisplayName("مدیریت کاربران")]
    public class RoleManagerController : BaseController
    {
        private readonly IApplicationRoleManager _roleManager;

        private const string RoleNotFound = "نقش یافت نشد.";
        public RoleManagerController(IApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));

        }

        //[HttpGet, DisplayName("نمایش نقش ها")]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRoles([DataSourceRequest] DataSourceRequest request)
        {

            DataSourceResult resultAsync = await _roleManager.GetAllRolesAndUsersCount().ToDataSourceResultAsync(request);
            return Json(resultAsync);
        }

        [HttpGet, AjaxOnly]
        public IActionResult RenderCreate()
        {
            var roleViewModel = new RolesViewModel();
            return PartialView(roleViewModel);
        }
        [HttpPost, AjaxOnly]
        public async Task<IActionResult> Create(RolesViewModel viewModel)
        {

            ReturnJson returnJson = new ReturnJson();
            if (ModelState.IsValid)
            {
                IdentityResult result;

                AppRole roleModel = new AppRole(viewModel.Name)
                {
                    Description = viewModel.Description
                };
                result = await _roleManager.CreateAsync(roleModel);

                if (result.Succeeded)
                {
                    returnJson.MessageType = MessageType.Success;
                    returnJson.Script = "RoleGridRefresh()";

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

            return Json(returnJson);
        }

        [HttpGet, AjaxOnly]
        public async Task<IActionResult> RenderEdit(int id)
        {
            var roleViewModel = new RolesViewModel();
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                roleViewModel.Id = role.Id;
                roleViewModel.Name = role.Name;
                roleViewModel.Description = role.Description;
            }

            return PartialView(roleViewModel);
        }

        [HttpPost, AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RolesViewModel viewModel)
        {

            ReturnJson returnJson = new ReturnJson();
            if (ModelState.IsValid)
            {
                IdentityResult result;

                AppRole role = await _roleManager.FindByIdAsync(viewModel.Id.ToString());
                role.Name = viewModel.Name;
                role.Description = viewModel.Description;
                result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    returnJson.MessageType = MessageType.Success;
                    returnJson.Script = "RoleGridRefresh()";

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
            return Json(returnJson);
        }

        [HttpGet, AjaxOnly]

        public async Task<IActionResult> RenderDelete(int id)
        {
            var roleViewModel = new RolesViewModel();
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                roleViewModel.Id = role.Id;
                roleViewModel.Name = role.Name;
                roleViewModel.Description = role.Description;
            }

            return PartialView(roleViewModel);
        }

        [HttpPost, AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RolesViewModel viewModel)
        {
            ReturnJson returnJson = new ReturnJson();

            var role = await _roleManager.FindByIdAsync(viewModel.Id.ToString());
            if (role == null)
            {
                returnJson.MessageType = MessageType.Error;
                returnJson.Message.Add(NotRoleFounded);
            }
            else
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    returnJson.MessageType = MessageType.Success;
                    returnJson.Script = "RoleGridRefresh()";
                }
                else
                {
                    returnJson.MessageType = MessageType.Error;
                    returnJson.Message.Add(result.DumpErrors());
                }

            }

            return Json(returnJson);
        }

        [HttpGet, AjaxOnly]

        public async Task<IActionResult> RenderDetail(int id)
        {
            var roleViewModel = new RolesViewModel();
            AppRole role = await _roleManager.FinRoleAndUsersCountById(id);
            if (role != null)
            {
                roleViewModel.UsersCount = role.Users.Count;
                roleViewModel.Name = role.Name;
                roleViewModel.Description = role.Description;
            }

            return PartialView(roleViewModel);
        }
    }
}