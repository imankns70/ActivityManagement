using System.Collections.Generic;
using ActivityManagement.DomainClasses.Entities.Identity;


namespace ActivityManagement.ViewModels.DynamicAccess
{
    public class DynamicAccessIndexViewModel
    {
        public string ActionIds { set; get; }
        public int RoleId { set; get; }
        public AppRole RoleIncludeRoleClaims { set; get; }
        public ICollection<ControllerViewModel> SecuredControllerActions { set; get; }
    }
}
