using System;

namespace ActivityManagement.DomainClasses.Entities.Business
{
    public class TeamSetting
    {
        public int TeamSettingId { get; set; }
        public string Title { get; set; }
        public bool CheckSupervisor { get; set; }
        public int UserId { get; set; }
        public virtual Team Team{get;set;}
        public int TeamId {get;set;}
         
             

         
    }
}