using ActivityManagement.ViewModels.SiteSettings;

namespace ActivityManagement.ViewModels.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(BreadCrumbViewModel breadCrumbViewModel)
        {
             BreadCrumbViewModel = breadCrumbViewModel;
        }
        public SiteInformation SiteInformation { get; set; }
        public BreadCrumbViewModel BreadCrumbViewModel { get; set; }
    }
}