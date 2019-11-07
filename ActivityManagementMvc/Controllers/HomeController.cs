using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.Controllers
{
    public class HomeController : BaseController
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}