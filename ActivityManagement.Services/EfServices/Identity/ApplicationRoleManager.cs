using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.RoleManager;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace ActivityManagement.Services.EfServices.Identity
{
    public class ApplicationRoleManager : RoleManager<AppRole> , IApplicationRoleManager
    {
        private readonly IdentityErrorDescriber _errors;
        private readonly IApplicationUserManager _userManager;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationRoleManager> _logger;
        private readonly IEnumerable<IRoleValidator<AppRole>> _roleValidators;
        private readonly IRoleStore<AppRole> _store;

        public ApplicationRoleManager(
            IRoleStore<AppRole> store,
            ILookupNormalizer keyNormalizer,
            ILogger<ApplicationRoleManager> logger,
            IEnumerable<IRoleValidator<AppRole>> roleValidators,
            IdentityErrorDescriber errors,
            IApplicationUserManager userManager) :
            base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _errors = errors;
            _errors.CheckArgumentIsNull(nameof(_errors));

            _keyNormalizer = keyNormalizer;
            _keyNormalizer.CheckArgumentIsNull(nameof(_keyNormalizer));

            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(_logger));

            _store = store;
            _store.CheckArgumentIsNull(nameof(_store));

            _roleValidators = roleValidators;
            _roleValidators.CheckArgumentIsNull(nameof(_roleValidators));

            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));
        }


        public List<AppRole> GetAllRoles()
        {
            return Roles.ToList();
        }

        public List<AppRole> GetAllRolesWithClaims()
        {
            return Roles.Include(appRole=> appRole.Claims).ToList();
        }


        public List<RolesViewModel> GetAllRolesAndUsersCount()
        {
            return Roles.Select(role =>
                             new RolesViewModel
                             {
                                 Id = role.Id,
                                 Name = role.Name,
                                 Description = role.Description,
                                 UsersCount = role.Users.Count(),
                             }).ToList();
        }

        public Task<AppRole> FindClaimsInRole(int roleId)
        {
            return Roles.Include(c => c.Claims).FirstOrDefaultAsync(c => c.Id == roleId);
        }

        public async Task<AppRole> FinRoleAndUsersCountById(int roleId)
        {
            return await Roles.Include(s => s.Users).FirstOrDefaultAsync(a => a.Id == roleId);
        }
        public async Task<IdentityResult> AddOrUpdateClaimsAsync(int roleId,string roleClaimType,IList<string> selectedRoleClaimValues)
        {
            var role = await FindClaimsInRole(roleId);
            if(role==null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "NotFound",
                    Description = "نقش مورد نظر یافت نشد.",
                });
            }

            var currentRoleClaimValues = role.Claims.Where(r => r.ClaimType == roleClaimType).Select(r => r.ClaimValue).ToList();
            if (selectedRoleClaimValues == null)
                selectedRoleClaimValues = new List<string>();

            var newClaimValuesToAdd = selectedRoleClaimValues.Except(currentRoleClaimValues).ToList();
            foreach(var claim in newClaimValuesToAdd)
            {
                role.Claims.Add(new RoleClaim
                {
                    RoleId=roleId,
                    ClaimType=roleClaimType,
                    ClaimValue=claim,
                });
            }

            var removedClaimValues = currentRoleClaimValues.Except(selectedRoleClaimValues).ToList();
            foreach(var claim in removedClaimValues)
            {
                var roleClaim = role.Claims.SingleOrDefault(r => r.ClaimValue == claim && r.ClaimType == roleClaimType);
                if (roleClaim != null)
                    role.Claims.Remove(roleClaim);
            }

            return await UpdateAsync(role);
        }

        public async Task<bool> CheckUserInThisRole(AppRole role)
        {
          return  await Roles.Include(a => a.Users).AnyAsync(a => a.Id == role.Id && a.Users.Any());
        }

        public async Task<List<UsersViewModel>> GetUsersInRoleAsync(int roleId)
        {
            var userIds = (from r in Roles
                           where (r.Id == roleId)
                           from u in r.Users
                           select u.UserId).ToList();

            return await _userManager.Users.Where(user => userIds.Contains(user.Id))
                .Select(user => new UsersViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    IsActive = user.IsActive,
                    Image = user.Image,
                    RegisterDateTime = user.RegisterDateTime,
                    Roles = user.Roles,
                }).AsNoTracking().ToListAsync();
        }


        //public async Task<List<RolesViewModel>> GetPaginateRolesAsync(int offset, int limit, bool? roleNameSortAsc, string searchText)
        //{
        //    List<RolesViewModel> roles;
        //    roles = await Roles.Where(r => r.Name.Contains(searchText)).Select(role => new RolesViewModel
        //    {
        //        Id = role.Id,
        //        Name = role.Name,
        //        Description = role.Description,
        //        UsersCount = role.Users.Count()
        //    }).Skip(offset).Take(limit).ToListAsync();

        //    if (roleNameSortAsc != null)
        //       roles = roles.OrderBy(t => (roleNameSortAsc == true) ? t.Name : "").ThenByDescending(t => (roleNameSortAsc == false) ? t.Name : "").ToList();

        //    foreach (var item in roles)
        //        item.Row = ++offset;

        //    return roles;
        //}

     


     }
}
