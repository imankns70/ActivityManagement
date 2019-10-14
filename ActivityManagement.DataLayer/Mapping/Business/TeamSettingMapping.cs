using ActivityManagement.DomainClasses.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityManagement.DataLayer.Mapping.Business
{
    public class TeamSettingMapping : IEntityTypeConfiguration<TeamSetting>
    {
        public void Configure(EntityTypeBuilder<TeamSetting> builder)
        {
           builder.ToTable("TeamSetting");
           builder.HasKey(x=>x.TeamSettingId);
           builder.Property(x=>x.CheckSupervisor).IsRequired(false);
           builder.HasOne(x=>x.Team).WithMany(c=>c.TeamSettings).HasForeignKey(c=>c.TeamId);
        }
    }
}