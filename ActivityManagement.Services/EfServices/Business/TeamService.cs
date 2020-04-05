using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Team> _teamService;
        private readonly IUserTeamService _userTeams;

        public TeamService(IUnitOfWork unitOfWork, IUserTeamService userTeams)
        {
            _unitOfWork = unitOfWork;
            _userTeams = userTeams;
            _teamService = _unitOfWork.Set<Team>();

        }

        public async Task<TeamViewModel> FindTeamByIdAsync(int id)
        {
            return await _teamService.Where(team => team.TeamId == id)
                .Select(team => new TeamViewModel
                {
                    TeamId = team.TeamId,
                    Name = team.Name,
                    Description = team.Description
                })
                .FirstOrDefaultAsync();
        }
        private bool IsExist(TeamViewModel viewModel)
        {
            return viewModel.TeamId != 0 ? _teamService.Any(a => a.TeamId != viewModel.TeamId && a.Name == viewModel.Name.Trim())
                 : _teamService.Any(a => a.Name == viewModel.Name.Trim());

        }
        public List<TeamViewModel> GetAllTeam()
        {
            return _teamService.Select(a => new TeamViewModel
            {
                TeamId = a.TeamId,
                Name = a.Name,
                Description = a.Description
            }).ToList();
        }

        public async Task<LogicResult> CreateAsync(TeamViewModel viewModel)
        {
            LogicResult logicResult = new LogicResult();
            if (IsExist(viewModel))
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.DuplicateRecord);
            }
            else
            {
                Team team = new Team
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,

                };
                _teamService.Add(team);
                int rowEffect = await _unitOfWork.SaveChangesAsync();
                if (rowEffect == 0)
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(NotificationMessages.InvalidRecord);
                }
                else
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Message.Add(NotificationMessages.CreateSuccess);
                }

            }

            return logicResult;

        }




        public async Task<LogicResult> EditAsync(TeamViewModel viewModel)
        {
            LogicResult logicResult = new LogicResult();
            if (IsExist(viewModel))
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.DuplicateRecord);
            }
            else
            {
                Team team = await _teamService.FirstOrDefaultAsync(a => a.TeamId == viewModel.TeamId);
                team.Name = viewModel.Name;
                team.Description = viewModel.Description;
                _teamService.Update(team);
                int rowEffect = await _unitOfWork.SaveChangesAsync();

                if (rowEffect == 0)
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(NotificationMessages.InvalidRecord);
                }
                else
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Message.Add(NotificationMessages.EditSuccess);
                }


            }

            return logicResult;
        }

        public async Task<LogicResult> DeleteAsync(TeamViewModel viewModel)
        {
            LogicResult logicResult = new LogicResult();
            Team team = await _teamService.FirstOrDefaultAsync(a => a.TeamId == viewModel.TeamId);
            if (team != null)
            {
                _teamService.Remove(team);
                int rowEffect = await _unitOfWork.SaveChangesAsync();
                if (rowEffect == 0)
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(NotificationMessages.InvalidRecord);
                }
                else
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Message.Add(NotificationMessages.DeleteSuccess);
                }
            }
            else
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.RecordNotFounded);
            }

            return logicResult;
        }

        public async Task<LogicResult> SetLeader(TeamViewModel viewModel)
        {
            LogicResult logicResult = new LogicResult();
            Team team = await _teamService.Include(s=>s.Users).FirstOrDefaultAsync(a => a.TeamId == viewModel.TeamId);
            if (team != null)
            {
                await _userTeams.SetOffAllTeam(viewModel.UserId);

                team.Users.Add( new UserTeam
                {
                    TeamId = viewModel.TeamId,
                    UserId = viewModel.UserId,
                    IsLeader = true,
                    IsCurrentTeam = true
                });
                _teamService.Update(team);
                int rowEffect = await _unitOfWork.SaveChangesAsync();
                if (rowEffect == 0)
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(NotificationMessages.InvalidRecord);
                }
                else
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Message.Add(NotificationMessages.CreateSuccess);
                }
            }
            else
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.Add(NotificationMessages.RecordNotFounded);
            }

            return logicResult;
        }

      
    }
}