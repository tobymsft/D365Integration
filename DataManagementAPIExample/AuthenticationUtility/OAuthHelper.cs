using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace DataManagementAPI.AuthenticationUtility
{
    public class OAuthHelper
    {
        /// <summary>
        /// The header to use for OAuth.
        /// </summary>
        public const string OAuthHeader = "Authorization";

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public static string GetAuthenticationHeader()
        {
            // Create and get JWT token
            return GetAuthenticationResult().CreateAuthorizationHeader();
        }

        

        public static AuthenticationResult GetAuthenticationResult()
        {
            string aadTenant = ClientConfiguration.Default.ActiveDirectoryTenant;
            string aadClientAppId = ClientConfiguration.Default.ActiveDirectoryClientAppId;
            string aadClientAppSecret = ClientConfiguration.Default.ActiveDirectoryClientAppSecret;
            string aadResource = ClientConfiguration.Default.ActiveDirectoryResource;

            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant);

            ClientCredential creadential = new ClientCredential(aadClientAppId, aadClientAppSecret);
            AuthenticationResult authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, creadential).Result;
            
            return authenticationResult;
        }
    }
}