//using Microsoft.AspNetCore.Mvc;
//using NewsChannel.Common.Attributes;

using ActivityManagement.Common.Attributes;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.Controllers
{


    public class BaseController : Controller
    {
        
        public const string InsertSuccess = "درج اطلاعات با موفقیت انجام شد.";
        public const string EditSuccess = "ویرایش اطلاعات با موفقیت انجام شد.";
        public const string DeleteSuccess = "حذف اطلاعات با موفقیت انجام شد.";
        public const string OperationSuccess = "عملیات با موفقیت انجام شد.";
        public const string InvalidImage = "عکس نامعتبر است.";

      
        public IActionResult Notification()
        {
            return Content(TempData["notification"].ToString());
        }
        [HttpGet, AjaxOnly]
        public IActionResult DeleteGroup()
        {
            return PartialView("_DeleteGroup");
        }

      

    }
}