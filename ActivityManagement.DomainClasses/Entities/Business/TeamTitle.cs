using System;

namespace ActivityManagement.DomainClasses.Entities.Business
{
    public class TeamTitle
    {
        public int TeamTitleId { get; set; }
        public string Title { get; set; }
        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}