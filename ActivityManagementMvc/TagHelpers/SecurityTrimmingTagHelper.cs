using ActivityManagement.Services.EfInterfaces.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ActivityManagementMvc.TagHelpers
{
    [HtmlTargetElement("security-trimming")]
    public class SecurityTrimmingTagHelper : TagHelper
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;

        public SecurityTrimmingTagHelper(ISecurityTrimmingService securityTrimmingService)
        {
            _securityTrimmingService = securityTrimmingService;
        }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }


        [HtmlAttributeName("asp-area")]
        public string Area { get; set; }


        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            if (!ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
            }

            string[] actions = Action.Split(":");
            foreach (var action in actions)
            {
                if (_securityTrimmingService.CanCurrentUserAccess(Area, Controller, action))
                {
                    return;
                }
            }
           

            output.SuppressOutput();
        }
    }
}
