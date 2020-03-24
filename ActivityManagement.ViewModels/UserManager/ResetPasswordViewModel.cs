using System.ComponentModel.DataAnnotations;

namespace ActivityManagement.ViewModels.UserManager
{
    public class ResetPasswordViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [Display(Name = "کلمه عبور جدید")]
        public string NewPassword { get; set; }
    }
}
