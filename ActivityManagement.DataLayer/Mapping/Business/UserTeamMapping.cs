using ActivityManagement.DomainClasses.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivityManagement.DataLayer.Mapping.Business
{
    public class UserTeamMapping : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
             builder.ToTable("UserTeam");
             builder.HasKey(x=> new {x.UserId, x.TeamId});
             
             

        }
    }
}