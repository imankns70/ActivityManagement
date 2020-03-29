using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.DataLayer.Context;
using ActivityManagement.DomainClasses.Entities.Business;
using ActivityManagement.Services.EfInterfaces.Business;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.Team;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagement.Services.EfServices.Business
{
    public class TeamService : ITeamService
    {
        private readonly DbSet<Team> _teams;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _teams = unitOfWork.Set<Team>();
        }

        private bool IsExist(TeamViewModel viewModel)
        {
           
           return viewModel.TeamId != 0 ? _teams.Any(a => a.TeamId != viewModel.TeamId && a.Name == viewModel.Name.Trim()) 
                : _teams.Any(a => a.Name == viewModel.Name.Trim());

         
        }
        public List<TeamViewModel> GetAllTeam()
        {
            return _teams.Select(a => new TeamViewModel
            {
                TeamId = a.TeamId,
                Name = a.Name,
                Description = a.Description
            }).ToList();
        }

        public Task<LogicResult> CreateAsyncTask(TeamViewModel viewModel)
        {
            LogicResult logicResult= new LogicResult();
            if (IsExist(viewModel))
            {
                logicResult.MessageType = MessageType.Error;
                    //logicResult
            }
            else
            {
                
            }

         
            throw new System.NotImplementedException();
        }
    }
}