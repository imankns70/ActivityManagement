using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ActivityManagement.DomainClasses.Entities.Business;
using Microsoft.AspNetCore.Identity;

namespace ActivityManagement.DomainClasses.Entities.Identity
{

    public class AppUser: IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Image { get; set; }
        public DateTime? RegisterDateTime { get; set; }
        public DateTime? LastVisitDateTime { get; set; }

        public bool IsActive { get; set; }
        public string Mobile { get; set; }
        public GenderType? Gender { get; set; }
    
        public virtual ICollection<UserTeam> Teams { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }

    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,

        [Display(Name = "زن")]
        Female = 2
    }
}