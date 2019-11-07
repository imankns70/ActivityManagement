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

        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<List<UsersViewModel>> GetAllUsersWithRolesAsync()
        {
            return await Users.Select(user => new UsersViewModel
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
                AccessFailedCount = user.AccessFailedCount,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                Gender = user.Gender,
            }).FirstOrDefaultAsync();
        }

        public async Task<string> GetFullName(ClaimsPrincipal User)
        {
            var UserInfo = await GetUserAsync(User);
            return UserInfo.FirstName + " " + UserInfo.LastName;
        }


        public async Task<List<UsersViewModel>> GetPaginateUsersAsync(int offset, int limit, bool? firstnameSortAsc, bool? lastnameSortAsc, bool? emailSortAsc, bool? usernameSortAsc,bool? registerDateTimeSortAsc, string searchText)
        {
            var users = await Users.Include(u => u.Roles).Where(t => t.FirstName.Contains(searchText) ||
                                                                     t.LastName.Contains(searchText) ||
                                                                     t.Email.Contains(searchText) ||
                                                                     t.UserName.Contains(searchText) || 
                                                                     t.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت hh:mm:ss").Contains(searchText))
                    .Select(user => new UsersViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        //Bio = user.Bio,
                        IsActive = user.IsActive,
                        Image = user.Image,
                        PersianBirthDate = user.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd"),
                        PersianRegisterDateTime = user.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss"),
                        GenderName = user.Gender == GenderType.Male ? "مرد" : "زن",
                        RoleId = user.Roles.Select(r => r.Role.Id).FirstOrDefault(),
                        RoleName = user.Roles.Select(r => r.Role.Name).FirstOrDefault()
                    }).Skip(offset).Take(limit).ToListAsync();

            if (firstnameSortAsc != null)
            {
                users = users.OrderBy(t => (firstnameSortAsc == true) ? t.FirstName : "").ThenByDescending(t => (firstnameSortAsc == false) ? t.FirstName : "").ToList();
            }

            else if (lastnameSortAsc != null)
            {
                users = users.OrderBy(t => (lastnameSortAsc == true) ? t.LastName : "").ThenByDescending(t => (lastnameSortAsc == false) ? t.LastName : "").ToList();
            }

            else if (emailSortAsc != null)
            {
                users = users.OrderBy(t => (emailSortAsc == true) ? t.Email : "").ThenByDescending(t => (emailSortAsc == false) ? t.Email : "").ToList();
            }

            else if (usernameSortAsc != null)
            {
                users = users.OrderBy(t => (usernameSortAsc == true) ? t.PhoneNumber : "").ThenByDescending(t => (usernameSortAsc == false) ? t.UserName : "").ToList();
            }

            else if (registerDateTimeSortAsc != null)
            {
                users = users.OrderBy(t => (registerDateTimeSortAsc == true) ? t.PersianRegisterDateTime : "").ThenByDescending(t => (registerDateTimeSortAsc == false) ? t.PersianRegisterDateTime : "").ToList();
            }

            foreach (var item in users)
                item.Row = ++offset;
             
            return users;
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
    }
}
