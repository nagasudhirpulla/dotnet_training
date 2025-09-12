using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieAuthDemo.Pages.Account;

public class LogoutModel : PageModel
{
    public IActionResult OnGet()
    {
        if (!(HttpContext.User?.Identity?.IsAuthenticated ?? false))
        {
            // redirect to home if user is not authenticated
            return RedirectToPage($"/{nameof(Index)}");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage($"/{nameof(Index)}");
    }
}
