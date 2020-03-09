using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ActivityManagement.ViewModels.RoleManager
{
    public class RolesViewModel
    {
       
        public int? Id { get; set; }
 

        [Display(Name="عنوان نقش")]
        [Required(ErrorMessage ="وارد نمودن {0} الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Description { get; set; }
                
        [Display(Name="تعداد کاربران")]
        public int UsersCount { get; set; }

    }
}
