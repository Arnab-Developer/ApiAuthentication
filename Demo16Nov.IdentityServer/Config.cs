using Duende.IdentityServer.Models;

namespace Demo16Nov.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("Demo16NovApi", "Demo16NovApi")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("Demo16NovApi", "Demo16NovApi")
            {
                Scopes = { "Demo16NovApi" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "Demo16NovWeb",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "Demo16NovApi" },
                Claims = { new ClientClaim("Admin", "Admin") }
            }
        };
}