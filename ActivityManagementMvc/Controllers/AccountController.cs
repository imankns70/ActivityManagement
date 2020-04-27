using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.UserManager;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ActivityManagementMvc.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly IWebHostEnvironment _env;

        public AccountController(IApplicationUserManager userManager, IApplicationRoleManager roleManager,
            SignInManager<AppUser> signInManager, ILogger<AccountController> logger, IHttpContextAccessor accessor,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _accessor = accessor;
            _env = env;
        }
        // GET
        public IActionResult SignIn(string returnUrl = null)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(viewModel.UserName);
                if (user != null)
                {
                    if (user.IsActive)
                    {

                        var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, true);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");

                        else if (result.IsLockedOut)
                            ModelState.AddModelError(string.Empty, "حساب کاربری شما به مدت 20 دقیقه به دلیل تلاش های ناموفق قفل شد.");

                        else if (result.RequiresTwoFactor)
                            return RedirectToAction("SendCode", new { RememberMe = viewModel.RememberMe });

                        else
                        {
                            ModelState.AddModelError(string.Empty, "نام کاربری یا کلمه عبور شما صحیح نمی باشد.");
                            _logger.LogWarning($"The user attempts to login with the IP address({_accessor.HttpContext?.Connection?.RemoteIpAddress.ToString()}) and username ({viewModel.UserName}) and password ({viewModel.Password}).");
                        }
                    }
                    else
                        ModelState.AddModelError(string.Empty, "حساب کابری شما غیرفعال است.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "نام کاربری یا کلمه عبور شما صحیح نمی باشد.");
                    _logger.LogWarning($"The user attempts to login with the IP address({_accessor.HttpContext?.Connection?.RemoteIpAddress.ToString()}) and username ({viewModel.UserName}) and password ({viewModel.Password}).");
                }
            }
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            string userId = User.Identity.GetUserId();
            AppUser user = await _userManager.FindByIdAsync(userId);
            await _signInManager.CanSignInAsync(user);
            return RedirectToAction("SignIn", "Account");
        }
    }
}