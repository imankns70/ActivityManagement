using System.Threading.Tasks;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.Controllers
{
    public class DynamicAccessController : BaseController
    {
        public readonly IApplicationRoleManager _roleManager;
        public readonly IMvcActionsDiscoveryService _mvcActionsDiscovery;
        public DynamicAccessController(IApplicationRoleManager roleManager, IMvcActionsDiscoveryService mvcActionsDiscovery)
        {
            _roleManager = roleManager;
            _mvcActionsDiscovery = mvcActionsDiscovery;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int roleId)
        {
            if (roleId == 0)
                return NotFound();


            var role = await _roleManager.FindClaimsInRole(roleId);
            if (role == null)
                return NotFound();

            var securedControllerActions = _mvcActionsDiscovery.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            return View(new DynamicAccessIndexViewModel
            {
                RoleIncludeRoleClaims = role,
                SecuredControllerActions = securedControllerActions,
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DynamicAccessIndexViewModel ViewModel)
        {
            var result = await _roleManager.AddOrUpdateClaimsAsync(ViewModel.RoleId, ConstantPolicies.DynamicPermissionClaimType, ViewModel.ActionIds.Split(","));
            if (!result.Succeeded)
                ModelState.AddModelError(string.Empty, "در حین انجام عملیات خطایی رخ داده است.");

            return RedirectToAction("Index", new { userId = ViewModel.RoleId });
        }
    }
}