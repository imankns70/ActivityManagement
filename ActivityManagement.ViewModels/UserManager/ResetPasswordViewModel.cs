using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace ActivityManagement.ViewModels.UserManager
{
    public class ResetPasswordViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage= "کلمه عبور باید حداقل شامل یک کاراکتر عدد ('0'-'9') باشد. کلمه عبور باید حداقل شامل یکی از کاراکترهای حرفی ('a'-'z') باشد. کلمه عبور باید حداقل شامل یک حرف بزرگ ('A'-'Z') باشد. کلمه عبور باید حداقل شامل 6 کاراکتر باشد.")]
        [StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [Display(Name = "کلمه عبور جدید")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password), Display(Name = "تکرار کلمه عبور")]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور وارد شده با تکرار کلمه عبور مطابقت ندارد.")]
        public string ConfirmNewPassword { get; set; }
    }
}
