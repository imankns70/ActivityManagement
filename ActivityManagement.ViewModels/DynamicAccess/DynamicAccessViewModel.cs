using System.Collections.Generic;
using ActivityManagement.DomainClasses.Entities.Identity;


namespace ActivityManagement.ViewModels.DynamicAccess
{
    public class DynamicAccessIndexViewModel
    {
        public string ActionIds { set; get; }
        public int UserId { set; get; }
        public AppUser UserIncludeUserClaims { set; get; }
        public ICollection<ControllerViewModel> SecuredControllerActions { set; get; }
    }
}
