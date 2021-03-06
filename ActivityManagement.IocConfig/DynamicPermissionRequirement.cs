﻿using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using ActivityManagement.Services.EfInterfaces.Identity;
using System.Threading.Tasks;
using ActivityManagement.Common.Api;
using ActivityManagement.IocConfig.Api.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ActivityManagement.IocConfig
{
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }

    public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;


        public DynamicPermissionsAuthorizationHandler(ISecurityTrimmingService securityTrimmingService)
        {
            _securityTrimmingService = securityTrimmingService;

        }

        protected override Task HandleRequirementAsync(
             AuthorizationHandlerContext context,
             DynamicPermissionRequirement requirement)
        {
            var mvcContext = context.Resource as Endpoint;

            var actionDescriptor = mvcContext?.Metadata.OfType<ControllerActionDescriptor>().SingleOrDefault();

            if (actionDescriptor != null)
            {
                actionDescriptor.RouteValues.TryGetValue("area", out var areaName);
                var area = string.IsNullOrWhiteSpace(areaName) ? string.Empty : areaName;

                actionDescriptor.RouteValues.TryGetValue("controller", out var controllerName);
                var controller = string.IsNullOrWhiteSpace(controllerName) ? string.Empty : controllerName;

                actionDescriptor.RouteValues.TryGetValue("action", out var actionName);
                var action = string.IsNullOrWhiteSpace(actionName) ? string.Empty : actionName;

                if (_securityTrimmingService.CanCurrentUserAccess(area, controller, action))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new AppException(ApiResultStatusCode.RedirectToHome, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);


                }
            }

            return Task.CompletedTask;
        }
    }
}
