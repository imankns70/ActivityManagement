using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.ViewModels.Home;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
 

namespace ActivityManagementMvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IWritableOptions<SiteSettings> _writableLocations;
        private readonly IHostingEnvironment _env;

        public HomeController(IWritableOptions<SiteSettings> writableOptions, IHostingEnvironment env)
        {
            _writableLocations = writableOptions;
            _env = env;

        }
        // GET
        public IActionResult Index()
        {
            SiteInformation siteInformation= new SiteInformation
            {
                Title = _writableLocations.Value.SiteInformation.Title,
                Favicon = _writableLocations.Value.SiteInformation.Favicon,
                Description = _writableLocations.Value.SiteInformation.Description,
                Logo = _writableLocations.Value.SiteInformation.Logo,
            };
          
            HomeViewModel homeViewModel= new HomeViewModel(siteInformation, new BreadCrumbViewModel());
            return View(homeViewModel);
        }
    }
}