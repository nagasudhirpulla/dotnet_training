## CookiesDemo
Demo for using cookies in ASP.NET Core

* `HttpContext` (Microsoft.AspNetCore.Http.HttpContext) object in dotnet contains all the information about the current HTTP request and response
* `HttpContext` is made available in the request pipeline by the dotnet framework
* Hence it can be accessed in any middleware, controller, or Razor page

## Create / Update a cookie

```cs
Response.Cookies.Append("dataKey", "dataValStr");
```

## Read a cookie
```cs
var cookieValue = Request.Cookies["dataKey"];
```

## References
* https://www.learnrazorpages.com/razor-pages/tutorial/bakery7/cookies 