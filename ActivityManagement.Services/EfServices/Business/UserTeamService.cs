using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.DataLayer.Context;
using ActivityManagement.DomainClasses.Entities.Business;
using ActivityManagement.Services.EfInterfaces.Business;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagement.Services.EfServices.Business
{
    public class UserTeamService : IUserTeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<UserTeam> _userTeams;
        public UserTeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userTeams = _unitOfWork.Set<UserTeam>();
        }

        public async Task SetOffAllTeam(int userId)
        {
            List<UserTeam> userTeams = await _userTeams.Where(a => a.UserId == userId).ToListAsync();
            userTeams.ForEach(userTeam => userTeam.IsCurrentTeam = false);
            await _unitOfWork.SaveChangesAsync();
            await Task.CompletedTask;

        }
    }
}