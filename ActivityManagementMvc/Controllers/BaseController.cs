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
        private readonly IWritableOptions<SiteSettings> _writableLocations;
        public const string InsertSuccess = "درج اطلاعات با موفقیت انجام شد.";
        public const string EditSuccess = "ویرایش اطلاعات با موفقیت انجام شد.";
        public const string DeleteSuccess = "حذف اطلاعات با موفقیت انجام شد.";
        public const string OperationSuccess = "عملیات با موفقیت انجام شد.";
        public const string InvalidImage = "عکس نامعتبر است.";

        public BaseController(IWritableOptions<SiteSettings> writableOptions)
        {
            _writableLocations = writableOptions;
        }
        public IActionResult Notification()
        {
            return Content(TempData["notification"].ToString());
        }
        [HttpGet, AjaxOnly]
        public IActionResult DeleteGroup()
        {
            return PartialView("_DeleteGroup");
        }

        public SiteInformation GetSitInformation()
        {
            return new SiteInformation
            {
                Title = _writableLocations.Value.SiteInformation.Title,
                Favicon = _writableLocations.Value.SiteInformation.Favicon,
                Description = _writableLocations.Value.SiteInformation.Description,
                Logo = _writableLocations.Value.SiteInformation.Logo,
            };
        }

    }
}