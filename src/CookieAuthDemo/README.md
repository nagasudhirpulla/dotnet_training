## CookieAuthDemo
* Demo for authenticating user with Cookie using `HttpContext.SignInAsync`

* `HttpContext` (Microsoft.AspNetCore.Http.HttpContext) object in dotnet contains all the information about the current HTTP request and response
* `HttpContext` is made available in the request pipeline by the dotnet framework
* Hence it can be accessed in any middleware, controller, or Razor page
* User can be logged in by calling `HttpContext.SignInAsync` method like

```csharp
await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties)
```
This creates an authentication cookie and sends it to the user's browser. Also a session is created for the user in the server.

* User can be logged out by calling `HttpContext.SignOutAsync` method like
```csharp
await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
```
This clears the authentication cookie from the user's browser and removes the user's session in the server.

* User claims are stored in the authentication cookie. Hence user claims can be accessed in any middleware, controller, or Razor page using `HttpContext.User` like
```csharp
// get primary ClaimsIdentity and see if user is authenticated
var isLoggedIn = HttpContext.User?.Identity?.IsAuthenticated;

// get user claims from all ClaimsIdentities of the user
var userClaims = HttpContext.User?.Claims;
```

## References
* https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0
* https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authentication/cookie/samples/6.x/CookieSample/Pages/Account/Login.cshtml.cs