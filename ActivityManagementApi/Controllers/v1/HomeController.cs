using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManagement.Common.Api;
using ActivityManagement.Common.Api.Attributes;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.SiteSettings;
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
        private readonly IWritableOptions<SiteSettings> _writableOptions;
         

        public HomeController(IWritableOptions<SiteSettings> writableOptions)
        {
            _writableOptions = writableOptions;
           
        }
        [HttpGet]
        public ApiResult<SiteInformation> Get()
        {
            SiteInformation siteInformation = new SiteInformation
            {
                Logo = _writableOptions.Value.SiteInformation.Logo,
                Description = _writableOptions.Value.SiteInformation.Description,
                Favicon = _writableOptions.Value.SiteInformation.Favicon,
                Title = _writableOptions.Value.SiteInformation.Title
            };
            
            return Ok(siteInformation);
        }
    }
}