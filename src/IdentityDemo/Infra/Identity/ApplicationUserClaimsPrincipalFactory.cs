using IdentityDemo.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace IdentityDemo.Infra.Identity;

public class ApplicationUserClaimsPrincipalFactory(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<AppUser, IdentityRole>(userManager, roleManager, optionsAccessor)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        // Map Username to the standard Name claim
        identity.AddClaim(new Claim(ClaimTypes.GivenName, user.DisplayName));

        return identity;
    }
}