using ActivityManagement.DomainClasses.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagement.DataLayer.Mapping
{
    public static class IdentityMapping
    {
        public static void AddCustomIdentityMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().ToTable("AppUsers").Property(x => x.Gender).IsRequired(false);
            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<UserRole>().ToTable("AppUserRole");
            modelBuilder.Entity<RoleClaim>().ToTable("AppRoleClaim");
            modelBuilder.Entity<UserClaim>().ToTable("AppUserClaim");

            modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens").Property(x => x.ClientId).IsRequired();
            modelBuilder.Entity<RefreshToken>().Property(x =>x.ClientId).IsRequired();
            modelBuilder.Entity<RefreshToken>().Property(x =>  x.ExpireDate).IsRequired();
            modelBuilder.Entity<RefreshToken>().Property(x => x.Value).IsRequired();
                
            modelBuilder.Entity<AppUser>()
                .HasMany(user => user.RefreshTokens)
                .WithOne(token => token.User).HasForeignKey(r => r.UserId).IsRequired();

            modelBuilder.Entity<UserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(role => role.Users).HasForeignKey(r => r.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(userRole => userRole.User)
                .WithMany(role => role.Roles).HasForeignKey(r => r.UserId);

            modelBuilder.Entity<RoleClaim>()
                .HasOne(roleClaim => roleClaim.Role)
                .WithMany(claim => claim.Claims).HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<UserClaim>()
                .HasOne(userClaim => userClaim.User)
                .WithMany(claim => claim.Claims).HasForeignKey(c => c.UserId);
        }
    }
}