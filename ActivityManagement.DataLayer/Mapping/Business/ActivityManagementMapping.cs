using Microsoft.EntityFrameworkCore;

namespace ActivityManagement.DataLayer.Mapping.Business
{
    public static class ActivityManagementMapping
    {
        public static void AddCustomActivityManagementMapping(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TeamMapping());
            builder.ApplyConfiguration(new TeamTitleMapping());
            builder.ApplyConfiguration(new ActivityMapping());
            builder.ApplyConfiguration(new TeamSettingMapping());
            builder.ApplyConfiguration(new UserTeamMapping());
        }
    }
}