using System.ComponentModel.DataAnnotations;

namespace ActivityManagement.ViewModels.Team
{
    public class TeamViewModel
    {
        public int TeamId { get; set; }
        [Display(Name = "نام تیم")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string Name { get; set; }
        [Display(Name = "تعدا قعالیت ها")]
        public int TeamActivities { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}