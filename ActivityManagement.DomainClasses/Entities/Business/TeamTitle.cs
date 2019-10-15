using System;

namespace ActivityManagement.DomainClasses.Entities.Business
{
    public class TeamTitle
    {
        public int TeamTitleId { get; set; }
        public string Title { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}