## CookieAuthDemo
* Demo for authenticating user with Cookies using `HttpContext.SignInAsync`

* `HttpContext` (Microsoft.AspNetCore.Http.HttpContext) object in dotnet contains all the information about the current HTTP request and response
* `HttpContext` is made available in the request pipeline by the dotnet framework
* Hence it can be accessed in any middleware, controller, or Razor page


## References
* https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0
* https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authentication/cookie/samples/6.x/CookieSample/Pages/Account/Login.cshtml.cs