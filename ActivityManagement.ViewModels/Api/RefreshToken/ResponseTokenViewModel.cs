using ActivityManagement.ViewModels.UserManager;

namespace ActivityManagement.ViewModels.Api.RefreshToken
{
    public class ResponseTokenViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
   
        public UserViewModelApi User { get; set; }
        
    }
}