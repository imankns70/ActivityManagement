using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common;
using ActivityManagement.Common.Attributes;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Business;
using ActivityManagement.ViewModels.Base;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.RoleManager;
using ActivityManagement.ViewModels.Team;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementMvc.Controllers
{
    [DisplayName("مدیریت تیم ها")]
    public class TeamManagerController : BaseController
    {
        private readonly ITeamService _teamService;

        public TeamManagerController(ITeamService teamService)
        {
            _teamService = teamService;
            _teamService.CheckArgumentIsNull(nameof(_teamService));
        }

        [HttpGet, DisplayName("نمایش تیم ها")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]

        public IActionResult Index()
        {

            return View();
        }
        [DisplayName("لیست نمایش تیم ها")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]

        public async Task<JsonResult> GetAllTeams([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult resultAsync = await _teamService.GetAllTeam().ToDataSourceResultAsync(request);
            return Json(resultAsync);
        }
        [HttpGet, AjaxOnly, DisplayName("نمایش ایجاد تیم")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult RenderCreate()
        {
            var teamViewModel = new TeamViewModel();
            return PartialView(teamViewModel);
        }

        [HttpPost, AjaxOnly, DisplayName("ذخیره اطلاعات تیم")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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
                logicResult.Message.Add(ModelState.GetErrorsModelState());
            }
            return Json(logicResult);
        }

        [HttpGet, AjaxOnly, DisplayName("نمایش ویرایش تیم")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RenderEdit(int id)
        {

            TeamViewModel viewModel = await _teamService.FindTeamByIdAsync(id);

            return PartialView(viewModel);
        }

        [HttpPost, AjaxOnly, DisplayName("ویرایش اطلاعات تیم")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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
                logicResult.Message.Add(ModelState.GetErrorsModelState());
            }
            return Json(logicResult);
        }

        [HttpGet, AjaxOnly, DisplayName("نمایش حذف تیم")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RenderDelete(int id)
        {
            TeamViewModel viewModel = await _teamService.FindTeamByIdAsync(id);

            return PartialView(viewModel);
        }

        [HttpPost, AjaxOnly, DisplayName("حذف اطلاعات تیم")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Delete(TeamViewModel viewModel)
        {
             
            LogicResult logicResult = await _teamService.DeleteAsync(viewModel);
            if (logicResult.MessageType == MessageType.Success)
            {
                logicResult.Script = "TeamGridRefresh()";
            }
            return Json(logicResult);
        }

        [HttpGet, AjaxOnly, DisplayName("نمایش جزئیات نقش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> RenderDetail(int id)
        {
            TeamViewModel viewModel = await _teamService.FindTeamByIdAsync(id);

            return PartialView(viewModel);
        }

        [HttpGet, AjaxOnly, DisplayName("نمایش انتخاب لیدر")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult RenderSetLeader(int id)
        {
            TeamViewModel viewModel= new TeamViewModel
            {
                TeamId = id,
               
            };

            return PartialView(viewModel);
        }
       
        [HttpPost, AjaxOnly, DisplayName("ذخیره انتخاب لیدر")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
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