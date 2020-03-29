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

        const string InsertSuccess = "درج اطلاعات با موفقیت انجام شد.";
        const string EditSuccess = "ویرایش اطلاعات با موفقیت انجام شد.";
        const string DeleteSuccess = "حذف اطلاعات با موفقیت انجام شد.";
        const string OperationSuccess = "عملیات با موفقیت انجام شد.";
        const string OperationFailed = "عملیات با خطا مواجه شد.";
        const string NotRoleFounded = "هیچ نقشی یافت نشد";
        const string UserNotFound = "هیچ کاربری یافت نشد";
        const string InvalidImage = "عکس نامعتبر است.";


    }
}