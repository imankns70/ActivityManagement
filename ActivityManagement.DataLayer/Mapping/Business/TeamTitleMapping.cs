using ActivityManagement.DomainClasses.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityManagement.DataLayer.Mapping.Business
{
    public class TeamTitleMapping : IEntityTypeConfiguration<TeamTitle>
    {
        public void Configure(EntityTypeBuilder<TeamTitle> builder)
        {
            builder.ToTable("TeamTitle");
            builder.HasKey(x => x.TeamTitleId);
            builder.HasOne(x => x.Team).WithMany(c => c.TeamTitles).HasForeignKey(b => b.TeamId);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");

        }
    }
}