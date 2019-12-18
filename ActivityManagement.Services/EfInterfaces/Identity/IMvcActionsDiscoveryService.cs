using System.Collections.Generic;
using ActivityManagement.ViewModels.DynamicAccess;

namespace ActivityManagement.Services.EfInterfaces.Identity
{
    public interface IMvcActionsDiscoveryService
    {
        ICollection<ControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName);
    }
}
