using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ActivityManagement.DomainClasses.Entities.Identity;
using Microsoft.AspNetCore.Http;

namespace ActivityManagement.ViewModels.UserManager
{
    public class UserViewModelApi
    {
        public int? Id { get; set; }

        public string Image { get; set; }
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string LastName { get; set; }

        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
        public string PersianBirthDate { get; set; }
        public string Password { get; set; }

        public GenderType? Gender { get; set; }

    }
}
