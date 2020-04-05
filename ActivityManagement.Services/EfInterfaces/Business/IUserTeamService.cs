using System.Threading.Tasks;

namespace ActivityManagement.Services.EfInterfaces.Business
{
    public interface IUserTeamService
    {
        /// <summary>
        /// غیر فعال کردن تمام تیم های کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task SetOffAllTeam(int userId);
    }
}