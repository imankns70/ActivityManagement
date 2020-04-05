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
    public class TeamManagerController : BaseController
    {
        private readonly ITeamService _teamService;

        public TeamManagerController(ITeamService teamService)
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
                logicResult = await _teamService.CreateAsync(viewModel);
                if (logicResult.MessageType == MessageType.Success)
                {
                    logicResult.Script = "TeamGridRefresh()";
                }

            }
            else
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message.AddRange(ModelState.GetErrorsModelState());
            }
            return Json(logicResult);
        }
        [HttpGet, AjaxOnly]
        public async Task<IActionResult> RenderEdit(int id)
        {

            TeamViewModel viewModel = await _teamService.FindTeamByIdAsync(id);

            return PartialView(viewModel);
        }

        [HttpPost, AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeamViewModel viewModel)
        {

            LogicResult logicResult = new LogicResult();
            if (ModelState.IsValid)
            {

                logicResult = await _teamService.EditAsync(viewModel);

                if (logicResult.MessageType == MessageType.Success)
                {
                    logicResult.Script = "TeamGridRefresh()";
                }

            }
            else
            {
                logicResult.MessageType = MessageType.Error;
                logicResult.Message = ModelState.GetErrorsModelState();
            }
            return Json(logicResult);
        }

        [HttpGet, AjaxOnly]

        public async Task<IActionResult> RenderDelete(int id)
        {
            TeamViewModel viewModel = await _teamService.FindTeamByIdAsync(id);

            return PartialView(viewModel);
        }

        [HttpPost, AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TeamViewModel viewModel)
        {
             
            LogicResult logicResult = await _teamService.DeleteAsync(viewModel);
            if (logicResult.MessageType == MessageType.Success)
            {
                logicResult.Script = "TeamGridRefresh()";
            }
            return Json(logicResult);
        }

        [HttpGet, AjaxOnly]
        public async Task<IActionResult> RenderDetail(int id)
        {
            TeamViewModel viewModel = await _teamService.FindTeamByIdAsync(id);

            return PartialView(viewModel);
        }
        [HttpGet, AjaxOnly]
        public IActionResult RenderSetLeader(int id)
        {
            TeamViewModel viewModel= new TeamViewModel
            {
                TeamId = id,
               
            };

            return PartialView(viewModel);
        }
        [HttpPost, AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetLeader(TeamViewModel viewModel)
        {

            LogicResult logicResult = await _teamService.SetLeader(viewModel);
            if (logicResult.MessageType == MessageType.Success)
            {
                logicResult.Script = "TeamGridRefresh()";
            }
            return Json(logicResult);
        }
    }
}