using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;

namespace AuthServer
{
    public class Config
    {
        public static List<TestUser> Users =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "818727",
                    Username = "alice",
                    Password = "alice",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        // Diğer claim'leri buraya ekleyebilirsiniz.
                    }
                },
                new TestUser
                {
                    SubjectId = "88421113",
                    Username = "bob",
                    Password = "bob",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        // Diğer claim'leri buraya ekleyebilirsiniz.
                    }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { JwtClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("weatherapi.read"),
                new ApiScope("weatherapi.write")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("weatherapi")
                {
                    Scopes = { "weatherapi.read", "weatherapi.write" },
                    ApiSecrets = { new IdentityServer4.Models.Secret("ScopeSecret".Sha256()) },
                    UserClaims = { JwtClaimTypes.Role }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "your-client-id",
                    ClientName = "Your Client Name",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new IdentityServer4.Models.Secret("myClientSecret".Sha256()) },
                    RedirectUris = { "https://your-client-redirect-uri" },
                    PostLogoutRedirectUris = { "https://your-client-post-logout-redirect-uri" },
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "weatherapi.read" }
                }
            };
    }
}
