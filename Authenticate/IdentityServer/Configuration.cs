using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Configuration
    {
        private static string OrdersApi = "OrdersAPI";

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    new [] {
                        OrdersApi
                    },
                },
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    new [] {
                        OrdersApi,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },

                    RedirectUris = { "https://localhost:8001/signin-oidc" }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(OrdersApi)
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResoures()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                // для Client.Mvc
                new IdentityResources.Profile()
            };
        }
    }
}
