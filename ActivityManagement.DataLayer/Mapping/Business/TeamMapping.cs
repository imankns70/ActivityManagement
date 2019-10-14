using ActivityManagement.DomainClasses.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityManagement.DataLayer.Mapping.Business
{
    public class TeamMapping : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Team");
            builder.HasKey(x => x.TeamId);
            
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(500);
           
        }
    }
}