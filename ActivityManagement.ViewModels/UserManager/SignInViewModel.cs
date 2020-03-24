using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagement.ViewModels.UserManager
{
    public class SignInViewModel 
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMe { get; set; }

        //[GoogleRecaptchaValidation]
        [BindProperty(Name = "g-recaptcha-response")]
        public string GoogleRecaptchaResponse { get; set; }

    }
}
