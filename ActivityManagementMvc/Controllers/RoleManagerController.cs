﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ActivityManagement.Common.Attributes;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.Home;
using ActivityManagement.ViewModels.RoleManager;
using ActivityManagement.ViewModels.SiteSettings;
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
        public async Task<JsonResult> GetRoles([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult resultAsync = await _roleManager.GetAllRoles().ToDataSourceResultAsync(request, x => new { x.Id, x.Name });
            return Json(resultAsync);
        }


        [HttpGet, AjaxOnly]
        public async Task<IActionResult> RenderRole(int? roleId)
        {
            var roleViewModel = new RolesViewModel();
            if (roleId != null)
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role != null)
                {
                    roleViewModel.Id = role.Id;
                    roleViewModel.Name = role.Name;
                    roleViewModel.Description = role.Description;
                }

                else
                    ModelState.AddModelError(string.Empty, RoleNotFound);
            }
            return PartialView("_RenderRole", roleViewModel);
        }


        [HttpPost, AjaxOnly]
        public async Task<IActionResult> CreateOrUpdate(RolesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                if (viewModel.Id != null)
                {
                    var role = await _roleManager.FindByIdAsync(viewModel.Id.ToString());
                    role.Name = viewModel.Name;
                    role.Description = viewModel.Description;
                    result = await _roleManager.UpdateAsync(role);
                }

                else
                {
                    AppRole roleModel = new AppRole(viewModel.Name)
                    {
                        Description = viewModel.Description
                    };
                    result = await _roleManager.CreateAsync(roleModel);

                }

                if (result.Succeeded)
                    TempData["notification"] = OperationSuccess;
                else
                    ModelState.AddErrorsFromResult(result);
            }
            return PartialView("_RenderRole", viewModel);
        }


        [HttpGet, AjaxOnly]
        public async Task<IActionResult> Delete(int? roleId)
        {
            if (roleId == null)
                ModelState.AddModelError(string.Empty, RoleNotFound);
            else
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                    ModelState.AddModelError(string.Empty, RoleNotFound);
                else
                {
                    RolesViewModel rolesViewModel = new RolesViewModel
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = role.Description,
                    };
                    return PartialView("_DeleteConfirmation", rolesViewModel);
                }

            }
            return PartialView("_DeleteConfirmation");
        }


        [HttpPost, ActionName("Delete"), AjaxOnly]
        public async Task<IActionResult> DeleteConfirmed(RolesViewModel viewModel)
        {
            var role = await _roleManager.FindByIdAsync(viewModel.Id.ToString());
            if (role == null)
                ModelState.AddModelError(string.Empty, RoleNotFound);
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    TempData["notification"] = DeleteSuccess;
                    return PartialView("_DeleteConfirmation", viewModel);
                }

                ModelState.AddErrorsFromResult(result);
            }
            return PartialView("_DeleteConfirmation");
        }


        [HttpPost, ActionName("DeleteGroup"), AjaxOnly]
        public async Task<IActionResult> DeleteGroupConfirmed(string[] btSelectItem)
        {
            if (!btSelectItem.Any())
                ModelState.AddModelError(string.Empty, "هیچ نقشی برای حذف انتخاب نشده است.");
            else
            {
                foreach (var item in btSelectItem)
                {
                    var role = await _roleManager.FindByIdAsync(item);
                    var result = await _roleManager.DeleteAsync(role);
                    if (!result.Succeeded)
                        ModelState.AddErrorsFromResult(result);

                }
                TempData["notification"] = "حذف گروهی نقش ها با موفقیت انجام شد..";
            }

            return PartialView("_DeleteGroup");
        }
    }
}