using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerSample2
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api","My Api"),
                new ApiResource("Home","My Home")
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId="client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api"}
                },
                new Client()
                {
                    ClientId="clientHome",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"Home"}
                },
                new Client()
                {
                    ClientId="pwdclient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api"}
                },
                new Client()
                {
                    ClientId="pwdclientHome",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "Home" }
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
