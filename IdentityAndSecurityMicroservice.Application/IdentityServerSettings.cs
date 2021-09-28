using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityAndSecurityMicroservice.Application
{
    public class IdentityServerSettings
    {
        public IReadOnlyCollection<ApiScope> ApiScopes { get; set; } = System.Array.Empty<ApiScope>();
        public IReadOnlyCollection<Client> Clients { get; init; }

        public IReadOnlyCollection<IdentityResource> IdentityResources { get; set; } =
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
    }
}
