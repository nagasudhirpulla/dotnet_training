using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CookieAuthDemo.Pages.Account;

public class LoginModel(ILogger<LoginModel> _logger) : PageModel
{
    public IActionResult OnGet(string? returnUrl)
    {
        if (HttpContext.User?.Identity?.IsAuthenticated ?? false)
        {
            return LocalRedirect(Url.GetLocalUrl(returnUrl));
            // You can also clear the existing cookie and signout user instead of redirecting
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        ReturnUrl = returnUrl ?? Url.Content("~/");
        return Page();
    }

    [BindProperty]
    public required InputModel Input { get; set; }

    public required string ReturnUrl { get; set; }

    public async Task<IActionResult> OnPostAsync(string? returnUrl)
    {
        ReturnUrl = returnUrl ?? Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = await AuthenticateUser(Input.Email, Input.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            var claims = new List<Claim>
                        {
                            new(ClaimTypes.Name, user.Email),
                            new("FullName", user.FullName),
                            new(ClaimTypes.Role, "Administrator"),
                        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            _logger.LogInformation("User {Email} logged in at {Time}.",
                user.Email, DateTime.UtcNow);

            return LocalRedirect(Url.GetLocalUrl(returnUrl));
        }

        // Something failed. Redisplay the form.
        return Page();
    }

    private static async Task<ApplicationUser?> AuthenticateUser(string email, string password)
    {

        // simulate 500 ms delay to mimic a database call
        await Task.Delay(500);

        // dummy user authentication logic
        if (email != "jamesbond@acme.com")
        {
            return null;
        }

        return new ApplicationUser()
        {
            Email = "jamesbond@acme.com",
            FullName = "James Bond"
        };
    }
}

public class InputModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}

public class ApplicationUser
{
    public required string Email { get; set; }
    public required string FullName { get; set; }
}

public static class UrlHelperExtensions
{
    public static string GetLocalUrl(this IUrlHelper urlHelper, string? localUrl)
    {
        if (!urlHelper.IsLocalUrl(localUrl))
        {
            // ! is the null-forgiving operator
            return urlHelper.Page($"/{nameof(Index)}")!;
        }

        return localUrl;
    }
}