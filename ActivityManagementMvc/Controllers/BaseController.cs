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
        public const string OperationFailed = "عملیات با خطا مواجه شد.";
        public const string NotRoleFounded = "هیچ نقشی یافت نشد";
        public const string UserNotFound = "هیچ کاربری یافت نشد";
        public const string InvalidImage = "عکس نامعتبر است.";
                     

    }
}