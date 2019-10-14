using System;
using ActivityManagement.DomainClasses.Entities.Identity;

namespace ActivityManagement.DomainClasses.Entities.Business
{
    public class UserTeam
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual AppUser User { get; set; }
    }
}