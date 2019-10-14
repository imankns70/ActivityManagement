using System;
using System.Collections.Generic;

namespace ActivityManagement.DomainClasses.Entities.Business
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TeamTitle> TeamTitles { get; set; }
        public virtual ICollection<TeamSetting> TeamSettings { get; set; }
        public virtual ICollection<UserTeam> Users { get; set; }
    }
}