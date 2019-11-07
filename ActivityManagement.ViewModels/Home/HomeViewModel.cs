using ActivityManagement.ViewModels.SiteSettings;

namespace ActivityManagement.ViewModels.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(SiteInformation siteInformation)
        {
            SiteInformation = siteInformation;
        }
        public SiteInformation SiteInformation { get; set; }
    }
}