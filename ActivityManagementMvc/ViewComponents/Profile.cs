using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.ViewComponents
{
    public class Profile : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
        
    }
}