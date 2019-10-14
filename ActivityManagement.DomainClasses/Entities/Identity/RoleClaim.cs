using Microsoft.AspNetCore.Identity;

namespace ActivityManagement.DomainClasses.Entities.Identity
{

    public class RoleClaim : IdentityRoleClaim<int>
    {
        public virtual AppRole Role { get; set; }
    }
}