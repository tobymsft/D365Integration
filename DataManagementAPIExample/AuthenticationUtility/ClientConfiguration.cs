using System;

namespace DataManagementAPI.AuthenticationUtility
{
    public class ClientConfiguration
    {
        public static ClientConfiguration Default => ClientConfiguration.Dev;

        public static ClientConfiguration Dev = new ClientConfiguration()
        {
            UriString = "https://<environment>.cloudax.dynamics.com/",
            ActiveDirectoryResource = "https://<environment>.cloudax.dynamics.com",
            ActiveDirectoryTenant = "https://login.microsoftonline.com/microsoft.com",
            ActiveDirectoryClientAppId = "",
            ActiveDirectoryClientAppSecret = "",
        };


        public string UriString { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        public string ActiveDirectoryClientAppSecret { get; set; }
    }
}
