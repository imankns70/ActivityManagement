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
        public readonly IApplicationUserManager _userManager;
        public readonly IMvcActionsDiscoveryService _mvcActionsDiscovery;
        public DynamicAccessController(IWritableOptions<SiteSettings> writableOptions, IApplicationUserManager userManager, IMvcActionsDiscoveryService mvcActionsDiscovery):base(writableOptions)
        {
            _userManager = userManager;
            _mvcActionsDiscovery = mvcActionsDiscovery;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {
            if (userId == 0)
                return NotFound();


            var user = await _userManager.FindClaimsInUser(userId);
            if (user == null)
                return NotFound();

            var securedControllerActions = _mvcActionsDiscovery.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            return View(new DynamicAccessIndexViewModel
            {
                UserIncludeUserClaims = user,
                SecuredControllerActions = securedControllerActions,
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DynamicAccessIndexViewModel ViewModel)
        {
            var Result = await _userManager.AddOrUpdateClaimsAsync(ViewModel.UserId, ConstantPolicies.DynamicPermissionClaimType, ViewModel.ActionIds.Split(","));
            if (!Result.Succeeded)
                ModelState.AddModelError(string.Empty, "در حین انجام عملیات خطایی رخ داده است.");

            return RedirectToAction("Index", new { userId = ViewModel.UserId });
        }
    }
}