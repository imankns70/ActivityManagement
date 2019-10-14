using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ActivityManagement.DomainClasses.Entities.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole(string name) : base(name) { }
        public AppRole(string name, string description) : base(name)
        {
            Description = description;
        }
        public string Description { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }
        public virtual ICollection<RoleClaim> Claims { get; set; }
    }
}