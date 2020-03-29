using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.Team;

namespace ActivityManagement.Services.EfInterfaces.Business
{
    public interface ITeamService
    {
        List<TeamViewModel> GetAllTeam();
        Task<LogicResult> CreateAsyncTask(TeamViewModel viewModel);

    }
}