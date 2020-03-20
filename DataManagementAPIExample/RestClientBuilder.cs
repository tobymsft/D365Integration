using System;
using DataManagementAPI.AuthenticationUtility;
using RestSharp;
using RestSharp.Authenticators;

namespace DataManagementAPI
{
    public static class RestClientBuilder
    {
        public static RestClient Client(string servicePath)
        {
            return new RestClient
            {
                BaseUrl = new Uri(servicePath),
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(OAuthHelper.GetAuthenticationResult().AccessToken, "Bearer")
            };
        }
    }
}
