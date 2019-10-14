using Microsoft.AspNetCore.Identity;

namespace ActivityManagement.DomainClasses.Entities.Identity
{
    public class UserClaim:IdentityUserClaim<int>
    {
        public virtual AppUser User { get; set; }
    }
}