using IdentityDemo.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace IdentityDemo.Pages;

[Authorize]
public class ToggleThemeModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : PageModel
{
    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        var user = await userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        var existingClaims = await userManager.GetClaimsAsync(user);
        var existingThemeClaim = existingClaims.FirstOrDefault(c => c.Type == "Theme");

        // remove existing theme claim if it exists
        if (existingThemeClaim != null)
        {
            await userManager.RemoveClaimAsync(user, existingThemeClaim);
        }

        // toggle theme
        string newTheme = existingThemeClaim?.Value == "light" ? "dark" : "light";

        // Add the new theme claim
        var newThemeClaim = new Claim("Theme", newTheme);
        await userManager.AddClaimAsync(user, newThemeClaim);

        // Refresh the user's sign-in cookie so the new claim takes effect immediately
        await signInManager.RefreshSignInAsync(user);
        
        return LocalRedirect(returnUrl);
    }

}
