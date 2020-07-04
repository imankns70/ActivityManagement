using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.ViewModels.RoleManager;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Identity;

namespace ActivityManagement.Services.EfInterfaces.Identity
{
    public interface IApplicationRoleManager
    {
        #region BaseClass
        IQueryable<AppRole> Roles { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IList<IRoleValidator<AppRole>> RoleValidators { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }
        Task<IdentityResult> CreateAsync(AppRole role);
        Task<IdentityResult> DeleteAsync(AppRole role);
        Task<AppRole> FindByIdAsync(string roleId);
        Task<AppRole> FindByNameAsync(string roleName);
        string NormalizeKey(string key);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> UpdateAsync(AppRole role);
        Task UpdateNormalizedRoleNameAsync(AppRole role);
        Task<string> GetRoleNameAsync(AppRole role);
        Task<IdentityResult> SetRoleNameAsync(AppRole role, string name);
        #endregion


        #region CustomMethod
        List<AppRole> GetAllRoles();
        Task<List<AppRole>> GetAllRolesWithClaimsAsync();
        List<RolesViewModel> GetAllRolesAndUsersCount();
        Task<AppRole> FindClaimsInRole(int roleId);
        Task<AppRole> FinRoleAndUsersCountById(int roleId);
        Task<List<UsersViewModel>> GetUsersInRoleAsync(int roleId);
        Task<IdentityResult> AddOrUpdateClaimsAsync(int roleId, string roleClaimType, IList<string> selectedRoleClaimValues);
        Task<bool> CheckUserInThisRole(AppRole role);

        #endregion
    }
}
