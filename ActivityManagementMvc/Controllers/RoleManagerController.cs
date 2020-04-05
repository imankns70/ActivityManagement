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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolesViewModel viewModel)
        {

            LogicResult logicResult = new LogicResult();
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
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Message.Add(NotificationMessages.CreateSuccess);
                    logicResult.Script = "RoleGridRefresh()";

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

            return Json(logicResult);
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

            LogicResult logicResult = new LogicResult();
            if (ModelState.IsValid)
            {
               

                AppRole role = await _roleManager.FindByIdAsync(viewModel.Id.ToString());
                role.Name = viewModel.Name;
                role.Description = viewModel.Description;
                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Message.Add(NotificationMessages.CreateSuccess);
                    logicResult.Script = "RoleGridRefresh()";

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
            return Json(logicResult);
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
            LogicResult logicResult = new LogicResult();

            var role = await _roleManager.FindByIdAsync(viewModel.Id.ToString());
            if (role == null)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.RoleNotFounded);
            }
            else
            {
                if (await _roleManager.CheckUserInThisRole(role))
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add("برای این نقش کاربر وجود دارد");
                }
                else
                {
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        logicResult.MessageType = MessageType.Success;
                        logicResult.Script = "RoleGridRefresh()";
                    }
                    else
                    {
                        logicResult.MessageType = MessageType.Error;
                        logicResult.Message.Add(result.DumpErrors());
                    }
                }
           
               

            }

            return Json(logicResult);
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

        [AjaxOnly]
        public async Task<IActionResult> ComboRole([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult resultAsync = await _roleManager.GetAllRoles()
                .Select(a=> new{ text=a.Name, value=a.Id}).ToDataSourceResultAsync(request);
             
            return Json(resultAsync);
        }
    }

    
}