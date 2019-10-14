using System;
using ActivityManagement.DomainClasses.Entities.Identity;

namespace ActivityManagement.DomainClasses.Entities.Business
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public int TeamId { get; set; }
        public int TeamTitleId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public DateTime SystemDate { get; set; }
        public DateTime? EditDate { get; set; }
        public bool Supervisor { get; set; }
        public DateTime? SupervisorDate { get; set; }
        public string SupervisorDescription { get; set; }
        public byte[] RowVersion { get; set; }
    }
}