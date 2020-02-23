namespace ActivityManagement.ViewModels.SiteSettings
{
    public class SiteSettings
    {
        public AdminUserSeed AdminUserSeed { get; set; }
        public SiteInformation SiteInformation { get; set; }
        public SiteEmail SiteEmail { get; set; }
        public JwtSettings JwtSettings { get; set; }
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
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string EncrypKey { get; set; }

        /// <summary>
        ///  صادر کننده توکن که باید اشاره کند به شرکت یا سایتی که آن را صادر می کند 
        /// </summary>
        /// <value>"SiteName.Com"</value>
        public string Issuer { get; set; }

        /// <summary>
        ///  جایی که قرار است از این توکت استفاده کند  
        /// </summary>
        /// <value>"SiteName.Com"</value>
        public string Audience { get; set; }

        /// <summary>
        ///  زمانی که قرار است از این توکت مورد پردازش قرار بگیرد  
        /// </summary>
        /// <value>"SiteName.Com"</value>
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }

    }
}
