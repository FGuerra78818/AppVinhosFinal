using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using AppVinhosFinal.Entities;  // ajusta o namespace conforme o teu projeto

namespace AppVinhosFinal.Services
{
    public class AppClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<UserAccount, IdentityRole<int>>
    {
        public AppClaimsPrincipalFactory(
            UserManager<UserAccount> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserAccount user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            // injecta o QuintaId
            if (user.QuintaId.HasValue)
                identity.AddClaim(new Claim("QuintaId", user.QuintaId.Value.ToString()));

            // injecta também o Role (o teu campo personalizado em UserAccount)
            if (!string.IsNullOrEmpty(user.Role))
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));

            return identity;
        }
    }
}
