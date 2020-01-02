namespace ActivityManagement.ViewModels.SiteSettings
{
    public class SiteSettings
    {
        public AdminUserSeed AdminUserSeed { get; set; }
        public SiteInformation SiteInformation { get; set; }
        public SiteEmail SiteEmail { get; set; }
    }


    public class AdminUserSeed
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SiteInformation
    {
        public string Title { get; set; }

        /// <summary>
        /// متا تگ سایت که مربوط به توضیحات سایت است
        /// </summary>
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Favicon { get; set; }
    }
    public class SiteEmail
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
