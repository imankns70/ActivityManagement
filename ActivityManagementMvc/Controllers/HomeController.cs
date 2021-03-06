﻿using System.ComponentModel;
using ActivityManagement.ViewModels.DynamicAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ActivityManagementMvc.Controllers
{
    [DisplayName("صفحه اصلی")]
    public class HomeController : BaseController
    {
        [HttpGet, DisplayName("نمایش صفحه اصلی")]
        //[Authorize(Policy= ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Error404()
        {

            return View();
        }
        public IActionResult Error()
        {
             
            return View();
        }
      
    }
}