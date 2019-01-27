using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityserverWithoutEntityFramework.Server
{
    public static class ConfigureIdentityServer
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResource(
                    name: "mv10blog.identity",
                    displayName: "MV10 Blog User Identify",
                    claimTypes: new[] { "mv10blog_accounttype" }
                )
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mv10blog.client",
                    ClientName = "McGuire10.com",
                    ClientUri = "http://localhost:5002/",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("the_secret".Sha256())
                    },
                    AllowRememberConsent = true,
                    AllowOfflineAccess = true,
                    RedirectUris =
                    {
                        "http://localhost:5002/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:5002/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Address,
                        "mv10blog.identity"
                    }
                }
            };
        }
    }
}
