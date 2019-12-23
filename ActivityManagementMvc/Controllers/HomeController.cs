﻿using System.ComponentModel;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.ViewModels.DynamicAccess;
using ActivityManagement.ViewModels.Home;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace ActivityManagementMvc.Controllers
{

   

    public class HomeController : BaseController
    {
        
        //[HttpGet, DisplayName("صفحه اصلی")]
        //[Authorize(Policy= ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {
           
            return View();
        }
    }
}