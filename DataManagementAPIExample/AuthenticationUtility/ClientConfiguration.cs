using System;

namespace DataManagementAPI.AuthenticationUtility
{
    public class ClientConfiguration
    {
        public static ClientConfiguration Default => ClientConfiguration.Dev;

        public static ClientConfiguration Dev = new ClientConfiguration()
        {
            UriString = "https://tjdev14f7ac1a0708ac504fdevaos.cloudax.dynamics.com/",
            ActiveDirectoryResource = "https://tjdev14f7ac1a0708ac504fdevaos.cloudax.dynamics.com",
            ActiveDirectoryTenant = "https://login.microsoftonline.com/microsoft.com",
            ActiveDirectoryClientAppId = "6a83cbe4-def8-466c-bba8-25174428823e",
            ActiveDirectoryClientAppSecret = "[Qk2Vabmq8g?p.:RpQERXx41imnYjn:a",
        };


        public string UriString { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        public string ActiveDirectoryClientAppSecret { get; set; }
    }
}
