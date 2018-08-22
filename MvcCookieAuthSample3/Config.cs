using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace MvcCookieAuthSample2
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api","My Api")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId="mvc",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("secret".Sha256())
                    },

                    RequireConsent = false,

                    RedirectUris = {"http://localhost:5001/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:5001/signout-callback-oidc"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "jesse",
                    Password = "123456"
                }
            };
        }
    }
}
