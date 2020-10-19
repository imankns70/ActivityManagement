using ActivityManagement.ViewModels.UserManager;

namespace ActivityManagement.ViewModels.Api.RefreshToken
{
    public class ResponseTokenViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccess { get; set; }
   
        public string Message { get; set; }
        public string Image { get; set; }
        public UserViewModelApi User { get; set; }
        
    }
}