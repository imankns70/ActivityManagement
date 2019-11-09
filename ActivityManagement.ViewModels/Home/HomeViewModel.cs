using ActivityManagement.ViewModels.SiteSettings;

namespace ActivityManagement.ViewModels.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(SiteInformation siteInformation, BreadCrumbViewModel breadCrumbViewModel)
        {
            SiteInformation = siteInformation;
            BreadCrumbViewModel = breadCrumbViewModel;
        }
        public SiteInformation SiteInformation { get; set; }
        public BreadCrumbViewModel BreadCrumbViewModel { get; set; }
    }
}