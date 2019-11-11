using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.ViewModels.Home;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace ActivityManagementMvc.Controllers
{
    public class HomeController : BaseController
    {


        private readonly IHostingEnvironment _env;

        public HomeController(IWritableOptions<SiteSettings> writableOptions, IHostingEnvironment env) : base(writableOptions)
        {

            _env = env;

        }
        // GET
        public IActionResult Index()
        {
            SiteInformation siteInformation = GetSitInformation();
            HomeViewModel homeViewModel = new HomeViewModel(siteInformation, new BreadCrumbViewModel());
            return View(homeViewModel);
        }
    }
}