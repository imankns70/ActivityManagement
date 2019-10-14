using ActivityManagement.DataLayer.Mapping;
using ActivityManagement.DataLayer.Mapping.Business;
using ActivityManagement.DomainClasses.Entities.Business;
using ActivityManagement.DomainClasses.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagement.DataLayer.Context {

    public class ActivityManagementContext : IdentityDbContext<AppUser,AppRole, int, UserClaim, UserRole, IdentityUserLogin<int>, RoleClaim, IdentityUserToken<int>>
    {
        
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamTitle> TeamTitles { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<Activity> Activities { get; set; }
         
        public ActivityManagementContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

             
          
            builder.AddCustomIdentityMappings();
            builder.AddCustomActivityManagementMapping();

            builder.Entity<Activity>().Property(x => x.SystemDate).HasDefaultValueSql("CONVERT(datetime,GetDate())");


        }

    }
}