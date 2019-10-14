using ActivityManagement.DomainClasses.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityManagement.DataLayer.Mapping.Business
{
    public class ActivityMapping : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activity");
            builder.HasKey(x => x.ActivityId);

            builder.HasOne(x => x.User).WithMany(c => c.Activities).HasForeignKey(b => b.UserId);
            builder.Property(x => x.RowVersion).IsConcurrencyToken();
            builder.Property(x => x.TeamId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.SystemDate).IsRequired();
            builder.Property(x => x.TeamTitleId).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.SupervisorDate).IsRequired(false);
            builder.Property(x => x.EditDate).IsRequired(false);
        }
    }
}