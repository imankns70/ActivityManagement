using Microsoft.AspNetCore.Identity;

namespace ActivityManagement.DomainClasses.Entities.Identity
{

    public class UserRole : IdentityUserRole<int>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}