using System.ComponentModel.DataAnnotations;

namespace ActivityManagement.ViewModels.Api.RefreshToken
{
    public class RequestTokenViewModel
    {
        [Required]
        public string GrantType { get; set; }
        [Required]
        public string ClientId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد.")]
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}