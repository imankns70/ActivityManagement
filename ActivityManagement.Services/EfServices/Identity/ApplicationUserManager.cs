using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.UserManager;
using ActivityManagement.Common;
using ActivityManagement.ViewModels.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ActivityManagement.Services.EfServices.Identity
{
    public class ApplicationUserManager : UserManager<AppUser>, IApplicationUserManager
    {
        private readonly ApplicationIdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationUserManager> _logger;
        private readonly IOptions<IdentityOptions> _options;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IEnumerable<IPasswordValidator<AppUser>> _passwordValidators;
        private readonly IServiceProvider _services;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IEnumerable<IUserValidator<AppUser>> _userValidators;
        //private readonly IMapper _mapper;

        public ApplicationUserManager(
            ApplicationIdentityErrorDescriber errors,
            ILookupNormalizer keyNormalizer,
            ILogger<ApplicationUserManager> logger,
            IOptions<IdentityOptions> options,
            IPasswordHasher<AppUser> passwordHasher,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators,
            IServiceProvider services,
            IUserStore<AppUser> userStore,
            IEnumerable<IUserValidator<AppUser>> userValidators)
            : base(userStore, options, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userStore = userStore;
            _errors = errors;
            _logger = logger;
            _services = services;
            _passwordHasher = passwordHasher;
            _userValidators = userValidators;
            _options = options;
            _keyNormalizer = keyNormalizer;
            _passwordValidators = passwordValidators;

        }

        public async Task<AppUser> FindUserWithRolesByNameAsync(string userName)
        {
            return await Users.Include(navProp => navProp.Roles)
                .FirstOrDefaultAsync(appUSer => appUSer.UserName == userName);
        }

        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<List<UsersViewModel>> GetAllUsersWithRolesAsync()
        {
            return await Users.Include(appUser => appUser.Roles).Select(user => new UsersViewModel
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

            }).ToListAsync();
        }
        public List<UsersViewModel> GetAllUsersWithRoles()
        {
            return Users.Include(user => user.Roles).Select(user => new UsersViewModel
            {

                Id = user.Id,
                LockOutEndCustom = user.LockoutEnd != null ? user.LockoutEnd.Value.DateTime.ToLocalTime() : (DateTime?)null,
                IsActive = user.IsActive,
                Image = user.Image,
                PersianBirthDate = user.BirthDate.ConvertGeorgianToPersian("yyyy/MM/dd"),
                PersianRegisterDateTime = user.RegisterDateTime.ConvertGeorgianToPersian("yyyy/MM/dd"),
                GenderName = user.Gender != null ? user.Gender == GenderType.Male ? "مرد" : "زن" : "",
                RoleName = user.Roles.Select(r => r.Role.Name).FirstOrDefault(),
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LockoutEnabled = user.LockoutEnabled,
                LastName = user.LastName,


            }).ToList();
        }

        public async Task<UsersViewModel> FindUserWithRolesByIdAsync(int userId)
        {
            return await Users.Where(u => u.Id == userId).Select(user => new UsersViewModel
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
                //RoleId = user.Roles.FirstOrDefault().RoleId,
                AccessFailedCount = user.AccessFailedCount,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                Gender = user.Gender,
            }).FirstOrDefaultAsync();
        }
        public async Task<UserViewModelApi> FindUserApiByIdAsync(int userId)
        {
            return await Users.Where(u => u.Id == userId).Select(user => new UserViewModelApi
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Image = user.Image,
                PersianBirthDate = user.BirthDate.HasValue ? user.BirthDate.ConvertGeorgianToPersian("yyyy/MM/dd") : "",
                Gender = user.Gender,
            }).FirstOrDefaultAsync();
        }
        public async Task<UsersViewModel> FindUserWithDetailIdAsync(int userId)
        {
            return await Users.Include(appUser => appUser.Roles).Where(u => u.Id == userId).Select(user => new UsersViewModel
            {
                Id = user.Id,
                LockOutEndCustom = user.LockoutEnd != null ? user.LockoutEnd.Value.DateTime.ToLocalTime() : (DateTime?)null,
                IsActive = user.IsActive,
                Image = user.Image,
                PersianBirthDate = user.BirthDate.ConvertGeorgianToPersian("yyyy/MM/dd"),
                PersianRegisterDateTime = user.RegisterDateTime.ConvertGeorgianToPersian("yyyy/MM/dd"),
                GenderName = user.Gender != null ? user.Gender == GenderType.Male ? "مرد" : "زن" : "",
                RoleName = user.Roles.Select(r => r.Role.Name).FirstOrDefault(),
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
            }).FirstOrDefaultAsync();
        }

        public async Task<string> GetFullName(ClaimsPrincipal user)
        {
            AppUser userInfo = await GetUserAsync(user);
            return userInfo.FirstName + " " + userInfo.LastName;
        }




        public string CheckAvatarFileName(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);
            int fileNameCount = Users.Count(f => f.Image == fileName);
            int j = 1;
            while (fileNameCount != 0)
            {
                fileName = fileName.Replace(fileExtension, "") + j + fileExtension;
                fileNameCount = Users.Count(f => f.Image == fileName);
                j++;
            }

            return fileName;
        }
        public Task<AppUser> FindClaimsInUser(int userId)
        {
            return Users.Include(c => c.Claims).FirstOrDefaultAsync(c => c.Id == userId);
        }


        public async Task<IdentityResult> AddOrUpdateClaimsAsync(int userId, string userClaimType, IList<string> selectedUserClaimValues)
        {
            var user = await FindClaimsInUser(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "NotFound",
                    Description = "کاربر مورد نظر یافت نشد.",
                });
            }

            var currentUserClaimValues = user.Claims.Where(r => r.ClaimType == userClaimType).Select(r => r.ClaimValue).ToList();
            if (selectedUserClaimValues == null)
                selectedUserClaimValues = new List<string>();

            var newClaimValuesToAdd = selectedUserClaimValues.Except(currentUserClaimValues).ToList();
            foreach (var claim in newClaimValuesToAdd)
            {
                user.Claims.Add(new UserClaim
                {
                    UserId = userId,
                    ClaimType = userClaimType,
                    ClaimValue = claim,
                });
            }

            var removedClaimValues = currentUserClaimValues.Except(selectedUserClaimValues).ToList();
            foreach (var claim in removedClaimValues)
            {
                var roleClaim = user.Claims.SingleOrDefault(r => r.ClaimValue == claim && r.ClaimType == userClaimType);
                if (roleClaim != null)
                    user.Claims.Remove(roleClaim);
            }

            return await UpdateAsync(user);
        }

        public async Task<LogicResult> UpdateUserProfile(UserViewModelApi viewModel)
        {
            LogicResult logicResult = new LogicResult();
            AppUser user = await FindByIdAsync(viewModel.Id.ToString());
            if (user != null)
            {
                logicResult.MessageType = MessageType.Success;
                logicResult.Message.Add(NotificationMessages.OperationSuccess);
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.UserName = viewModel.UserName;
                user.BirthDate = !string.IsNullOrWhiteSpace(viewModel.PersianBirthDate) ? viewModel.PersianBirthDate.ConvertPersianToGeorgian() : user.BirthDate;
                user.Gender = viewModel.Gender;
                user.Email = viewModel.Email;
                await UpdateAsync(user);
            }
            else
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.UserNotFound);
            }

            return logicResult;
        }
    }
}
