using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ActivityManagement.DomainClasses.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ActivityManagement.ViewModels.UserManager
{
    public class UsersViewModel
    {
  
        public int? Id { get; set; }
         
        [Display(Name = "تصویر پروفایل")]
        public string Image { get; set; }

        [Display(Name ="تصویر پروفایل")]
        //[Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public IFormFile ImageFile { get; set; }


        [Required(ErrorMessage ="وارد نمودن {0} الزامی است.")]
        [Display(Name="نام کاربری")]
        public string UserName { get; set; }
        
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده صحیح نمی باشد.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password), Display(Name = "کلمه عبور"),JsonIgnore]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور وارد شده با تکرار کلمه عبور مطابقت ندارد.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string LastName { get; set; }

        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string PersianBirthDate { get; set; }
        
        [Display(Name = "تاریخ عضویت"),]
        public DateTime? RegisterDateTime { get; set; }

        [Display(Name = "تاریخ عضویت")]
        public string PersianRegisterDateTime { get; set; }

        [Display(Name = "معرفی")]
        public string Bio { get; set; }
        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public GenderType? Gender { get; set; }

        [JsonProperty("جنسیت")]
        public string GenderName { get; set; }

        [JsonIgnore]
        public ICollection<UserRole> Roles { get; set; }
        [JsonIgnore]
        public List<AppRole> AllRoles { get; set; }

        [Display(Name ="نقش")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public int? RoleId { get; set; }

        [Display(Name = "نقش")]
        public string RoleName { get; set; }

        [JsonIgnore]
        public bool PhoneNumberConfirmed { get; set; }

        [JsonIgnore]
        public bool TwoFactorEnabled { get; set; }

        [JsonIgnore]
        public bool LockoutEnabled { get; set; }

        [JsonIgnore]
        public bool EmailConfirmed { get; set; }

        [JsonIgnore]
        public int AccessFailedCount { get; set; }

        [JsonIgnore]
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
