using System;
using ActivityManagement.DomainClasses.Entities.Identity;

namespace ActivityManagement.DomainClasses.Entities.Business
{
    public class UserTeam
    {
        public int UserTeamId { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public bool IsLeader { get; set; }
        public bool IsCurrentTeam { get; set; }
        public virtual Team Team { get; set; }
        public virtual AppUser User { get; set; }
    }
}