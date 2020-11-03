using ActivityManagement.Common.Api;
using ActivityManagement.ViewModels.UserManager;
using System.Collections.Generic;

namespace ActivityManagement.ViewModels.Api.RefreshToken
{
    public class ResponseTokenViewModel
    {
       
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode ApiStatusCode { get; set; }
        public string Message { get; set; }
   
        public UserViewModelApi User { get; set; }
        
    }
}