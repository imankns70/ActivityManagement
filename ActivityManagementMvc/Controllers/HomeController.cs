using System.ComponentModel;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.Home;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace ActivityManagementMvc.Controllers
{

    [DisplayName("خانه")]

    public class HomeController : BaseController
    {
        
        private readonly IHostingEnvironment _env;

        public HomeController(IWritableOptions<SiteSettings> writableOptions, IHostingEnvironment env) : base(writableOptions)
        {

            _env = env;

        }
        //[HttpGet, DisplayName("صفحه اصلی")]
        //[Authorize(Policy= ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {
            SiteInformation siteInformation = GetSitInformation();
            HomeViewModel homeViewModel = new HomeViewModel(siteInformation, new BreadCrumbViewModel());
            return View(homeViewModel);
        }
    }
}