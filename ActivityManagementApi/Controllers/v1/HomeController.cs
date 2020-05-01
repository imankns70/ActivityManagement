using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common.Api;
using ActivityManagement.Common.Api.Attributes;
using ActivityManagement.ViewModels.UserManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManagementApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiResultFilter]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public ApiResult<string> Get()
        {

             return Ok("my name is iman solouki and this the my own first api service project ");
        }
    }
}