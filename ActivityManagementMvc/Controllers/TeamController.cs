using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Attributes;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Business;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.RoleManager;
using ActivityManagement.ViewModels.Team;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.Controllers
{
    public class TeamController : BaseController 
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
            _teamService.CheckArgumentIsNull(nameof(_teamService));
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<JsonResult> GetAllTeams([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult resultAsync = await _teamService.GetAllTeam().ToDataSourceResultAsync(request);
            return Json(resultAsync);
        }
        [HttpGet, AjaxOnly]
        public IActionResult RenderCreate()
        {
            var teamViewModel = new TeamViewModel();
            return PartialView(teamViewModel);
        }

        [HttpPost, AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamViewModel viewModel)
        {

            LogicResult logicResult = new LogicResult();
            if (ModelState.IsValid)
            {
                IdentityResult result;

                AppRole roleModel = new AppRole(viewModel.Name)
                {
                    Description = viewModel.Description
                };
                result = await _teamService.CreateAsync(roleModel);

                if (result.Succeeded)
                {
                    logicResult.MessageType = MessageType.Success;
                    logicResult.Script = "RoleGridRefresh()";

                }
                else
                {
                    logicResult.MessageType = MessageType.Error;
                    logicResult.Message.Add(result.DumpErrors());

                }
            }
            else
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message = ModelState.GetErrorsModelState();
            }

            return Json(logicResult);
        }
    }
}