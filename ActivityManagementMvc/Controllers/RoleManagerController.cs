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

    [DisplayName("مدیریت نقش ها")]
    public class RoleManagerController : BaseController
    {
        private readonly IApplicationRoleManager _roleManager;
        public readonly IMvcActionsDiscoveryService _mvcActionsDiscovery;
        public RoleManagerController(IApplicationRoleManager roleManager,
            IMvcActionsDiscoveryService mvcActionsDiscoveryService)
        {
            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));

            _mvcActionsDiscovery = mvcActionsDiscoveryService;
            _mvcActionsDiscovery.CheckArgumentIsNull(nameof(_mvcActionsDiscovery));

        }

        [HttpGet, DisplayName("نمایش نقش ها")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {
            return View();
        }

        [DisplayName("لیست نمایش نقش ها")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<JsonResult> GetRoles([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult resultAsync = await _roleManager.GetAllRolesAndUsersCount().ToDataSourceResultAsync(request);
            return Json(resultAsync);
        }

        [HttpGet, AjaxOnly, DisplayName("نمایش ایجاد نقش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult RenderCreate()
        {
            var roleViewModel = new RolesViewModel();
            return PartialView(roleViewModel);
        }

        [HttpPost, AjaxOnly, DisplayName("ذخیره اطلاعات نقش")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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

        [HttpGet, AjaxOnly, DisplayName("نمایش ویرایش نقش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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

        [HttpPost, AjaxOnly, DisplayName("ویرایش اطلاعات نقش")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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

        [HttpGet, AjaxOnly, DisplayName("نمایش حذف نقش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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

        [HttpPost, AjaxOnly, DisplayName("حذف اطلاعات نقش")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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

        [HttpGet, AjaxOnly, DisplayName("نمایش جزئیات نقش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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


        [HttpGet, DisplayName("نمایش سطح دسترسی")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> DynamicAccess(int id)
        {
           
            AppRole role = await _roleManager.FindClaimsInRole(id);
            

            ICollection<ControllerViewModel> securedControllerActions = _mvcActionsDiscovery.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            return PartialView(new DynamicAccessIndexViewModel
            {
                RoleIncludeRoleClaims = role,
                SecuredControllerActions = securedControllerActions,
            });
        }



        [HttpPost, AjaxOnly, DisplayName("ارسال اطلاعات سطح دسترسی")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        //[JwtAuthentication(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> DynamicAccess(DynamicAccessIndexViewModel viewModel)
        {
            LogicResult logicResult = new LogicResult();
            var result = await _roleManager.AddOrUpdateClaimsAsync(viewModel.RoleId, ConstantPolicies.DynamicPermissionClaimType, viewModel.ActionIds);
            if (!result.Succeeded)
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(result.DumpErrors());
            }
            else
            {
                logicResult.MessageType = MessageType.Success;
                logicResult.Message.Add(NotificationMessages.CreateSuccess);
            }

            return Json(logicResult);
        }
        [AjaxOnly]
        public async Task<IActionResult> ComboRole([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult resultAsync = await _roleManager.GetAllRoles()
                .Select(a => new { text = a.Name, value = a.Id }).ToDataSourceResultAsync(request);

            return Json(resultAsync);
        }

    }

}