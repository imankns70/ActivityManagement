using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.Team;

namespace ActivityManagement.Services.EfInterfaces.Business
{
    public interface ITeamService
    {
        List<TeamViewModel> GetAllTeam();
        Task<TeamViewModel> FindTeamByIdAsync(int id);
        Task<LogicResult> CreateAsync(TeamViewModel viewModel);
        Task<LogicResult> EditAsync(TeamViewModel viewModel);
        Task<LogicResult> DeleteAsync(TeamViewModel viewModel);
        Task<LogicResult> SetLeader(TeamViewModel viewModel);
        
    }
}